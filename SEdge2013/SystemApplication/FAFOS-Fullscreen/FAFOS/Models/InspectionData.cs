using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Xml;
using System.IO;

namespace FAFOS
{
    class InspectionData
    {

        XmlDocument Inspection = null;
        XmlNode inspectionNode = null;
        XmlNode franchiseeNode = null;
        XmlNode clientNode = null;
        XmlNode clientContract = null;
        XmlNode serviceAddress = null;
        XmlNode Floor = null;
        XmlNode Room = null;
        XmlNode Extinguisher = null;
        XmlNode FireHoseCabinet = null;
        XmlNode EmergencyLight = null;



        public InspectionData(String[] data)
        {
            Inspection = new XmlDocument();
            inspectionNode = Inspection.CreateXmlDeclaration("1.0", null, null);
            Inspection.AppendChild(inspectionNode);

            franchiseeNode = Inspection.CreateElement("Franchisee");
            XmlAttribute franchiseeAttribute = Inspection.CreateAttribute("id");
            franchiseeAttribute.Value = data[0];
            franchiseeNode.Attributes.Append(franchiseeAttribute);

            franchiseeAttribute = Inspection.CreateAttribute("name");
            franchiseeAttribute.Value = data[1];
            franchiseeNode.Attributes.Append(franchiseeAttribute);

            Inspection.AppendChild(franchiseeNode);
        }

        public void addClient(String[] data)
        {
            //<Client> node
            clientNode = Inspection.CreateElement("Client");
            //id attribute
            XmlAttribute clientAttribute = Inspection.CreateAttribute("id");
            clientAttribute.Value = data[0];
            clientNode.Attributes.Append(clientAttribute);
            //name attribute
            clientAttribute = Inspection.CreateAttribute("name");
            clientAttribute.Value = data[1];
            clientNode.Attributes.Append(clientAttribute);
            //address attribute
            clientAttribute = Inspection.CreateAttribute("address");
            clientAttribute.Value = data[2];
            clientNode.Attributes.Append(clientAttribute);

            franchiseeNode.AppendChild(clientNode);

        }

        public void addContract(String[] data)
        {
            //<clientContract> node
            clientContract = Inspection.CreateElement("clientContract");
            //id attribute
            XmlAttribute clientContractAttribute = Inspection.CreateAttribute("id");
            clientContractAttribute.Value = data[0];
            clientContract.Attributes.Append(clientContractAttribute);
            //No attribute
            clientContractAttribute = Inspection.CreateAttribute("No");
            clientContractAttribute.Value = data[1];
            clientContract.Attributes.Append(clientContractAttribute);
            //startDate attribute
            clientContractAttribute = Inspection.CreateAttribute("startDate");
            clientContractAttribute.Value = data[2];
            clientContract.Attributes.Append(clientContractAttribute);
            //endtDate attribute
            clientContractAttribute = Inspection.CreateAttribute("endDate");
            clientContractAttribute.Value = data[3];
            clientContract.Attributes.Append(clientContractAttribute);
            //terms attribute
            clientContractAttribute = Inspection.CreateAttribute("terms");
            clientContractAttribute.Value = data[4];
            clientContract.Attributes.Append(clientContractAttribute);

            clientNode.AppendChild(clientContract);
        }

        public void addServiceAddress(String[] data)
        {
            //<clientContract> node
            serviceAddress = Inspection.CreateElement("ServiceAddress");
            //id attribute
            XmlAttribute serviceAddressAttribute = Inspection.CreateAttribute("id");
            serviceAddressAttribute.Value = data[0];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //address attribute
            serviceAddressAttribute = Inspection.CreateAttribute("address");
            serviceAddressAttribute.Value = data[1];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //postalCode attribute
            serviceAddressAttribute = Inspection.CreateAttribute("postalCode");
            serviceAddressAttribute.Value = data[2];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //contact attribute
            serviceAddressAttribute = Inspection.CreateAttribute("contact");
            serviceAddressAttribute.Value = data[3];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //city attribute
            serviceAddressAttribute = Inspection.CreateAttribute("city");
            serviceAddressAttribute.Value = data[4];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //province attribute
            serviceAddressAttribute = Inspection.CreateAttribute("province");
            serviceAddressAttribute.Value = data[5];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //country attribute
            serviceAddressAttribute = Inspection.CreateAttribute("country");
            serviceAddressAttribute.Value = data[6];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //InspectorID attribute
            serviceAddressAttribute = Inspection.CreateAttribute("InspectorID");
            serviceAddressAttribute.Value = data[7];
            serviceAddress.Attributes.Append(serviceAddressAttribute);
            //testTimeStamp attribute
            serviceAddressAttribute = Inspection.CreateAttribute("testTimeStamp");
            serviceAddressAttribute.Value = data[8];
            serviceAddress.Attributes.Append(serviceAddressAttribute);

            clientContract.AppendChild(serviceAddress);
        }

        public void addFloors(String[] data)
        {
            //<Floor> node
            Floor = Inspection.CreateElement("Floor");
            //name attribute
            XmlAttribute floorAttribute = Inspection.CreateAttribute("name");
            floorAttribute.Value = "Floor " + data[0];
            Floor.Attributes.Append(floorAttribute);

            serviceAddress.AppendChild(Floor);
        }

        public void addRooms(String[] data)
        {
            //<Room> node
            Room = Inspection.CreateElement("Room");
            //id attribute
            XmlAttribute roomAttribute = Inspection.CreateAttribute("id");
            roomAttribute.Value = data[0];
            Room.Attributes.Append(roomAttribute);
            //No attribute
            roomAttribute = Inspection.CreateAttribute("No");
            roomAttribute.Value = data[1];
            Room.Attributes.Append(roomAttribute);

            Floor.AppendChild(Room);
        }

        public void addExtinguisher(String[] data)
        {
            //<Extinguisher> node
            Extinguisher = Inspection.CreateElement("Extinguisher");
            //id attribute
            XmlAttribute ExtinguisherAttribute = Inspection.CreateAttribute("id");
            ExtinguisherAttribute.Value = data[0];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);
            //Location attribute
            ExtinguisherAttribute = Inspection.CreateAttribute("location");
            ExtinguisherAttribute.Value = data[1];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);
            //size attribute
            ExtinguisherAttribute = Inspection.CreateAttribute("size");
            ExtinguisherAttribute.Value = data[2];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);
            //type attribute
            ExtinguisherAttribute = Inspection.CreateAttribute("type");
            ExtinguisherAttribute.Value = data[3];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);
            //model attribute
            ExtinguisherAttribute = Inspection.CreateAttribute("model");
            ExtinguisherAttribute.Value = data[4];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);
            //serialNo attribute
            ExtinguisherAttribute = Inspection.CreateAttribute("serialNo");
            ExtinguisherAttribute.Value = data[5];
            Extinguisher.Attributes.Append(ExtinguisherAttribute);

            //<inspectionElement> node1
            XmlNode inspectionElement1 = Inspection.CreateElement("inspectionElement");
            XmlAttribute ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Hydro Test";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement1);
            //<inspectionElement> node2
            XmlNode inspectionElement2 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "6 Year Insp";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement2);
            //<inspectionElement> node3
            XmlNode inspectionElement3 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Weight";
            inspectionElement3.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement3.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement3.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement3);
            //<inspectionElement> node4
            XmlNode inspectionElement4 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Bracket";
            inspectionElement4.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement4.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement4.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement4);
            //<inspectionElement> node5
            XmlNode inspectionElement5 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Gauge";
            inspectionElement5.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement5.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement5.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement5);
            //<inspectionElement> node6
            XmlNode inspectionElement6 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Pull Pin";
            inspectionElement6.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement6.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement6.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement6);
            //<inspectionElement> node7
            XmlNode inspectionElement7 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Signage";
            inspectionElement7.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement7.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement7.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement7);
            //<inspectionElement> node8
            XmlNode inspectionElement8 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Collar";
            inspectionElement8.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement8.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement8.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement8);
            //<inspectionElement> node9
            XmlNode inspectionElement9 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Hose";
            inspectionElement9.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement9.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement9.Attributes.Append(ElementAttribute);
            Extinguisher.AppendChild(inspectionElement9);

            Room.AppendChild(Extinguisher);
        }

        public void addFireHoseCabinet(String[] data)
        {
            //<FireHoseCabinet> node
            FireHoseCabinet = Inspection.CreateElement("FireHoseCabinet");
            //id attribute
            XmlAttribute FireHoseCabinetAttribute = Inspection.CreateAttribute("id");
            FireHoseCabinetAttribute.Value = data[0];
            FireHoseCabinet.Attributes.Append(FireHoseCabinetAttribute);
            //Location attribute
            FireHoseCabinetAttribute = Inspection.CreateAttribute("location");
            FireHoseCabinetAttribute.Value = data[1];
            FireHoseCabinet.Attributes.Append(FireHoseCabinetAttribute);
            //manufacturingDate attribute
            FireHoseCabinetAttribute = Inspection.CreateAttribute("manufacturingDate");
            FireHoseCabinetAttribute.Value = data[2];
            FireHoseCabinet.Attributes.Append(FireHoseCabinetAttribute);

            //<inspectionElement> node1
            XmlNode inspectionElement1 = Inspection.CreateElement("inspectionElement");
            XmlAttribute ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Cabinet Condition";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            FireHoseCabinet.AppendChild(inspectionElement1);
            //<inspectionElement> node2
            XmlNode inspectionElement2 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Nozzle Condition";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            FireHoseCabinet.AppendChild(inspectionElement2);
            //<inspectionElement> node3
            XmlNode inspectionElement3 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Hose Re-Rack";
            inspectionElement3.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement3.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement3.Attributes.Append(ElementAttribute);
            FireHoseCabinet.AppendChild(inspectionElement3);
            //<inspectionElement> node4
            XmlNode inspectionElement4 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Hydrostatic Test Due";
            inspectionElement4.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement4.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement4.Attributes.Append(ElementAttribute);
            FireHoseCabinet.AppendChild(inspectionElement4);

            Room.AppendChild(FireHoseCabinet);
        }

        public void addEmergencyLight(String[] data)
        {
            //<EmergencyLight> node
            EmergencyLight = Inspection.CreateElement("EmergencyLight");
            //id attribute
            XmlAttribute EmergencyLightAttribute = Inspection.CreateAttribute("id");
            EmergencyLightAttribute.Value = data[0];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //Location attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("location");
            EmergencyLightAttribute.Value = data[1];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //model attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("model");
            EmergencyLightAttribute.Value = data[2];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //make attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("make");
            EmergencyLightAttribute.Value = data[3];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //numHeads attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("numHeads");
            EmergencyLightAttribute.Value = data[4];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //totalPower attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("totalPower");
            EmergencyLightAttribute.Value = data[5];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);
            //voltage attribute
            EmergencyLightAttribute = Inspection.CreateAttribute("voltage");
            EmergencyLightAttribute.Value = data[6];
            EmergencyLight.Attributes.Append(EmergencyLightAttribute);

            //<inspectionElement> node1
            XmlNode inspectionElement1 = Inspection.CreateElement("inspectionElement");
            XmlAttribute ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Requires Service or Repair";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement1.Attributes.Append(ElementAttribute);
            EmergencyLight.AppendChild(inspectionElement1);
            //<inspectionElement> node2
            XmlNode inspectionElement2 = Inspection.CreateElement("inspectionElement");
            ElementAttribute = Inspection.CreateAttribute("name");
            ElementAttribute.Value = "Operation Confirmed";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testResult");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            ElementAttribute = Inspection.CreateAttribute("testNote");
            ElementAttribute.Value = "";
            inspectionElement2.Attributes.Append(ElementAttribute);
            EmergencyLight.AppendChild(inspectionElement2);

            Room.AppendChild(EmergencyLight);
        }

        public void SaveInspection()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;

            settings.OmitXmlDeclaration = true;

            using (XmlWriter writer = XmlWriter.Create(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml", settings))
            {
                Inspection.Save(writer);
            }

        }

        public String SendInspection()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";
            settings.NewLineChars = "\r\n";
            settings.NewLineHandling = NewLineHandling.Replace;
            settings.OmitXmlDeclaration = true;

            StringWriter XMLData = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(XMLData, settings))
            {
                Inspection.Save(writer);
            }
            return XMLData.ToString();
        }
    }
}
