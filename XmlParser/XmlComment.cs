using System;
using System.Collections.Generic;
using System.Text;

namespace XmlParser {
  class XmlComment {
    public string Text { get; set; }
    public XmlComment() {

    }

    public XmlComment(string content) {
      this.Text = content;
    }
  }
}
