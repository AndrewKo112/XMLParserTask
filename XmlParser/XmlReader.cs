using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace XmlParser {
  class XmlParser {

    private enum XmlElementType { Open, Closed, OpenClosed };

    private static readonly Regex xmlTagRegex = new Regex(@"<\s*(?<closed>/?)\s*(?<tagName>\S+)(?<attributes>\s+[^/>]+)?(?<openClosed>/?)\s*>");
    private static readonly Regex xmlCommentRegex = new Regex(@"<!--(?<content>.*)-->");
    private static readonly Regex attributesRegex = new Regex(@"((?<key>[\S]+)=""(?<value>[^""]+)""\s*)");
    

    public static object ParseXml(string xml) {
      object documentNode = null;
      xml = xml.Trim();

      Stack<XmlNode> awaitingEnding = new Stack<XmlNode>();

      for (int i = 0; i < xml.Length; i++) {
        if (xml[i] == '<') {
          string tagStr = xml.Substring(i, xml.IndexOf('>', i) - i + 1);
          i += tagStr.Length - 1;

          object element = ParseElement(tagStr, out XmlElementType elementType);

          if (awaitingEnding.TryPeek(out XmlNode parent)) {
            // Link to parent if not 
            parent.AddElement(element);
          }
          else if (documentNode == null && element is XmlNode) {
            // Set current node as a document if it's the first one
            documentNode = element;
          }
          else {
            throw new FormatException("More that one root node provided");
          }

          if (elementType == XmlElementType.Open) {
            awaitingEnding.Push((XmlNode)element);
          }
          else if (elementType == XmlElementType.Closed) {
            awaitingEnding.Pop();
          }
        }
        else {
          var text = xml.Substring(i, xml.IndexOf('<', i) - i);
          i += text.Length - 1;

          if (awaitingEnding.TryPeek(out XmlNode parent)) {
            parent.AddElement(new XmlText(text));
          }
          else {
            throw new FormatException("There cannot be text in the root of the document");
          }
        }
      }

      return documentNode;
    }

    private static object ParseElement(string tagText, out XmlElementType elementType) {
      elementType = XmlElementType.Open;

      if (tagText.Contains("<!--")) {
        // XML Comment
        elementType = XmlElementType.OpenClosed;
        return new XmlComment(xmlCommentRegex.Match(tagText).Groups["content"].Value);
      }

      var match = xmlTagRegex.Match(tagText.Trim());
      if (match.Success) {
        var xmlNode = new XmlNode {
          Name = match.Groups["tagName"].Value
        };
        PopulateAttributes(xmlNode, match.Groups["attributes"].Value);

        if (match.Groups["closed"].Length > 0) {
          elementType = XmlElementType.Closed;
        }
        else if (match.Groups["openClosed"].Length > 0) {
          elementType = XmlElementType.OpenClosed;
        }

        return xmlNode;
      }

      throw new Exception("I'm to lazy to make up a description for this one");
    }

    private static void PopulateAttributes(XmlNode node, string attributesString) {

      if (String.IsNullOrWhiteSpace(attributesString)) {
        return;
      }

      attributesString = attributesString.Trim();

      foreach (Match match in attributesRegex.Matches(attributesString)) {
        node.Attributes.Add(match.Groups["key"].Value, match.Groups["value"].Value);
      }
    }
  }
}
