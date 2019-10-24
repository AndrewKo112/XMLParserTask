using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace XmlParser {
  class XmlNode {

    public string Name { get; set; }

    private readonly ArrayList _childElement = new ArrayList();
    private readonly Dictionary<string, string> _attributes = new Dictionary<string, string>();

    public Dictionary<string, string> Attributes { get => _attributes; }

    public XmlNode() {
    }

    public void AddElement(object element) => _childElement.Add(element);
    public void InsertElement(int index, object element) => _childElement.Insert(index, element);
    public void RemoveElement(object element) => _childElement.Remove(element);
    public void RemoveElementAt(int index) => _childElement.RemoveAt(index);

    public override string ToString() {
      return base.ToString();
    }
  }
}
