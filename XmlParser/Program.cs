using System;

namespace XmlParser {
  class Program {
    static void Main(string[] args) {
      var xml = @"<providerOrganization>
        <id root=""1.2.840.114350.1.13.519.2.7.2.688879"" extension=""103300"" />
        <name>Meritus</name>
        <telecom nullFlavor=""NA"" />
        <addr>
          <streetAddressLine>11116 Medical Campus Road</streetAddressLine>
          <city>Hagerstown</city>
          <state>MD</state>
          <postalCode>21742</postalCode>
          <country>US</country>
          <county>WASHINGTON</county>
        </addr>
      </providerOrganization>";

      XmlNode node = XmlParser.ParseXml(xml) as XmlNode;

      // Tasks:
      // 1. Create abstract class XmlElement
      //    All other xml element classes should be its derivatives
      //    XmlElement should contain interface that can represent all of element types
      //    Client should be able to use XmlReader and process xml data without knowing which type of an element they are processing
      //    Use the power of polymorphism
      //    P.S. XmlReader should operate with XmlElement type instead of object

      // 2. Implement possibility to stringify XML objects using ToString

      // 3. Create class that would allow to easily build xml documents in functional style
      // e.g. builder.AddNode("parent", new {a="attr"}).AddNode("child").AddText("test-test").Submit().AddNode("child-2").End()
      // would create xml <parent a="attr"><child>test-test<child><child-2/></parent>
      // Consider using one of the design patterns for nodes
    }
  }
}
