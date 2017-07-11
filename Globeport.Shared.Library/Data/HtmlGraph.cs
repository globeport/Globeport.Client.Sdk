using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Net;

using HtmlAgilityPack;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    /// <summary>
    /// Represents Open Graph meta data parsed from HTML
    /// </summary>
    public class HtmlGraph
    {
        public IDictionary<string, string> Data { get; } = new Dictionary<string, string>();

        public IList<string> LocalAlternatives { get; private set; } = new List<string>();

        public string Type { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public string Image { get; private set; }

        public string Url { get; private set; }


        private HtmlGraph()
        {
        }

        public HtmlGraph(string url)
        {
            Initialize(null, null, null, url);
        }

        public HtmlGraph(string url, string type)
        {
            Initialize(null, type, (type == "image" ? url : null), url);
        }

        public HtmlGraph(string title, string type, string image, string url, string description = "", string siteName = "", string audio = "", string video = "", string locale = "", IList<string> localeAlternate = null, string determiner = "")
        {
            Initialize(title, type, image, url, description, siteName, audio, video, locale, localeAlternate, determiner);
        }

        void Initialize(string title,string type,string image,string url,string description = "",string siteName = "",string audio = "",string video = "",string locale = "",IList<string> localeAlternatives = null,string determiner = "")
        {
            Title = title;
            Type = type;
            Image = image;
            Url = url;
            Description = description;
            LocalAlternatives = localeAlternatives;

            Data.Add("title", title);
            Data.Add("type", type);
            Data.Add("image", image);
            Data.Add("url", url);

            if (!string.IsNullOrWhiteSpace(description))
            {
                Data.Add("description", description);
            }

            if (!string.IsNullOrWhiteSpace(siteName))
            {
                Data.Add("site_name", siteName);
            }

            if (!string.IsNullOrWhiteSpace(audio))
            {
                Data.Add("audio", audio);
            }

            if (!string.IsNullOrWhiteSpace(video))
            {
                Data.Add("video", video);
            }

            if (!string.IsNullOrWhiteSpace(locale))
            {
                Data.Add("locale", locale);
            }

            if (!string.IsNullOrWhiteSpace(determiner))
            {
                Data.Add("determiner", determiner);
            }

            if (localeAlternatives != null)
            {
                LocalAlternatives = localeAlternatives;
            }
        }

        public override string ToString()
        {
            var doc = new HtmlDocument();

            foreach (var itm in Data)
            {
                var meta = doc.CreateElement("meta");
                meta.Attributes.Add("property", "og:" + itm.Key);
                meta.Attributes.Add("content", itm.Value);
                doc.DocumentNode.AppendChild(meta);
            }

            foreach (var itm in LocalAlternatives)
            {
                var meta = doc.CreateElement("meta");
                meta.Attributes.Add("property", "og:locale:alternate");
                meta.Attributes.Add("content", itm);
                doc.DocumentNode.AppendChild(meta);
            }

            return doc.DocumentNode.InnerHtml;
        }

        public static HtmlGraph ParseHtml(string url, string content)
        {
            var graph = new HtmlGraph();

            graph.Data.Add("url", url);
            graph.Data.Add("type", "text/html");

            int indexOfClosingHead = Regex.Match(content, "</head>").Index;
            string toParse = content.Substring(0, indexOfClosingHead + 7);

            toParse = toParse + "<body></body></html>\r\n";

            var document = new HtmlDocument();

            document.LoadHtml(toParse);

            var allMeta = document.DocumentNode.Descendants("meta");
            var urlPropertyPatterns = new[] { "image", "url^" };
            var openGraphMetaTags = from meta in allMeta ?? new HtmlNodeCollection(null)
                                    where (meta.Attributes.Contains("property") && meta.Attributes["property"].Value.StartsWith("og:")) ||
                                    (meta.Attributes.Contains("name") && meta.Attributes["name"].Value.StartsWith("og:"))
                                    select meta;

            foreach (var metaTag in openGraphMetaTags)
            {
                string value = GetOpenGraphValue(metaTag);
                string property = GetOpenGraphKey(metaTag);
                if (string.IsNullOrWhiteSpace(value))
                {
                    continue;
                }

                if (graph.Data.ContainsKey(property))
                {
                    continue;
                }

                foreach (var urlPropertyPattern in urlPropertyPatterns)
                {
                    if (Regex.IsMatch(property, urlPropertyPattern))
                    {
                        value = HtmlDecodeUrl(value);
                        break;
                    }
                }
                graph.Data.Add(property, value);
            }

            LoadMissingGraphData(graph, document);

            if (!graph.Data.ContainsKey("image"))
            {
                document.LoadHtml(content);
                GuessImage(document, graph);
            }

            graph.Type = graph.Data.GetValue("type");
            graph.Title = graph.Data.GetValue("title");
            graph.Description = graph.Data.GetValue("description");
            graph.Image = graph.Data.GetValue("image");
            graph.Url = graph.Data.GetValue("url");

            return graph;
        }

        static void LoadMissingGraphData(HtmlGraph graph, HtmlDocument document)
        {
            var headNode = document.DocumentNode.Descendants("head").FirstOrDefault();

            foreach (var node in headNode.ChildNodes)
            {
                switch (node.Name.ToLower())
                {
                    case "link":
                        break;
                    case "title":
                        if (!graph.Data.ContainsKey("title"))
                        {
                            var title = WebUtility.HtmlDecode(node.InnerText);
                            graph.Data.Add("title", title);
                        }
                        break;
                    case "meta":
                        if (!graph.Data.ContainsKey("title"))
                        {
                            if (node.Attributes["name"] != null && node.Attributes["content"] != null)
                            {
                                switch (node.Attributes["name"].Value.ToLower())
                                {
                                    case "description":
                                        var description = WebUtility.HtmlDecode(node.Attributes["content"].Value);
                                        graph.Data.Add("description", description);
                                        break;
                                }
                            }
                        }
                        break;
                }
            }

            if (!graph.Data.ContainsKey("image"))
            {
                // look for apple touch icon in header
                var imageNode = headNode.Descendants("link").FirstOrDefault(i => i.GetAttributeValue("rel", "") == "apple-touch-icon");
                if (imageNode != null)
                {
                    if (imageNode.Attributes["href"] != null)
                    {
                        var image = GetFullyQualifiedImage(imageNode.Attributes["href"].Value, graph.Data.GetValue("url"));
                        graph.Data.Add("image", image);
                    }
                    if (imageNode.Attributes["src"] != null)
                    {
                        var image = GetFullyQualifiedImage(imageNode.Attributes["src"].Value, graph.Data.GetValue("url"));
                        graph.Data.Add("image", image);
                    }
                }
                //look for link image in header
                imageNode = headNode.Descendants("link").FirstOrDefault(i => i.GetAttributeValue("rel", "") == "image_src");
                if (imageNode != null)
                {
                    if (imageNode.Attributes["href"] != null)
                    {
                        var image = GetFullyQualifiedImage(imageNode.Attributes["href"].Value, graph.Data.GetValue("url"));
                        graph.Data.Add("image", image);
                    }
                    if (imageNode.Attributes["src"] != null)
                    {
                        var image = GetFullyQualifiedImage(imageNode.Attributes["src"].Value, graph.Data.GetValue("url"));
                        graph.Data.Add("image", image);
                    }
                }
            }
        }

        static void GuessImage(HtmlDocument document, HtmlGraph graph)
        {
            var imageNodes = document.DocumentNode.Descendants("img");
            var h1 = string.Empty;
            var h1Node = document.DocumentNode.Descendants("h1").FirstOrDefault();
            if (h1Node != null)
            {
                h1 = h1Node.InnerText;
            }
            var bestScore = -1;
            if (imageNodes != null)
            {
                foreach (var imageNode in imageNodes)
                {
                    if (imageNode != null && imageNode.Attributes["src"] != null && imageNode.Attributes["alt"] != null)
                    {
                        var imageLink = GetFullyQualifiedImage(imageNode.Attributes["src"].Value, graph.Data.GetValue("url"));

                        var myScore = ScoreImage(imageNode.Attributes["alt"].Value, graph.Data.GetValue("title")??string.Empty);
                        myScore += ScoreImage(imageNode.Attributes["alt"].Value, h1);

                        if (myScore > bestScore)
                        {
                            graph.Data["image"] = imageLink;
                            bestScore = myScore;
                        }
                    }
                }
            }
        }

        //score the image based on matches in comparing alt to title and h1 tag
        static int ScoreImage(string text, string compare)
        {
            text = text.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            compare = compare.Replace("\r\n", string.Empty).Replace("\t", string.Empty);
            var score = 0;
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(compare))
            {
                var c = compare.Split(' ');

                foreach (var test in c)
                {
                    if (text.Contains(test)) { score++; }
                }
            }
            return score;
        }

        static string HtmlDecodeUrl(string value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            // naive attempt
            var patterns = new Dictionary<string, string>
            {
                ["&amp;"] = "&",
            };


            foreach (var key in patterns)
            {
                value = value.Replace(key.Key, key.Value);
            }

            return value;

        }

        static string GetOpenGraphKey(HtmlNode metaTag)
        {
            if (metaTag.Attributes.Contains("property"))
            {
                return CleanOpenGraphKey(metaTag.Attributes["property"].Value);
            }

            return CleanOpenGraphKey(metaTag.Attributes["name"].Value);
        }

        static string CleanOpenGraphKey(string value)
        {
            return value.Replace("og:", string.Empty).ToLowerInvariant();
        }

        static string GetOpenGraphValue(HtmlNode metaTag)
        {
            if (!metaTag.Attributes.Contains("content"))
            {
                return string.Empty;
            }

            return metaTag.Attributes["content"].Value;
        }

        static string GetFullyQualifiedImage(string imageUrl, string siteUrl)
        {
            if (imageUrl.Contains("http:") || imageUrl.Contains("https:"))
            {
                return imageUrl;
            }

            if (imageUrl.IndexOf("//") == 0)
            {
                return "http:" + imageUrl;
            }
            try
            {
                var baseurl = siteUrl.Replace("http://", string.Empty).Replace("https://", string.Empty);
                baseurl = baseurl.Split('/')[0];
                return $"http://{baseurl}{imageUrl}";

            }
            catch { }

            return imageUrl;

        }

        public bool IsValid
        {
            get
            {
                return Type != null && !Title.IsNullOrEmpty();
            }
        }
    }
}