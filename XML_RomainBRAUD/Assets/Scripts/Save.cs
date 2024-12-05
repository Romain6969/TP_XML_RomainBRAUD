using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using TMPro;
using System.Globalization;

public class Save : MonoBehaviour
{
    static string[] saveFileName = { "File1", "File2" };
    static int currentSaveFile;
    static string _name = "Romain";
    private static TimePlayed _timePlayed;
    static int _numberSavePressed;
    [SerializeField]TMP_Text Affichage;

    private void Awake()
    {
        _timePlayed = GetComponent<TimePlayed>();
        LoadGame();
        Affichage.text = $"Nom du joueur : Romain \nTemp de jeu : {_timePlayed.Timed()} \nNombre de foit appuyer sur SAVE : {_numberSavePressed}";
    }
    public void OnPress()
    {
        _numberSavePressed += 1;
        XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
        {
            NewLineOnAttributes = true,
            Indent = true,
        };
        Debug.Log(Application.persistentDataPath);
        XmlWriter writer = XmlWriter.Create("C:/Users/romai/Documents/Unity/TP_XML_RomainBRAUD/XML_RomainBRAUD/Assets/Scripts/File1/" + "/" + saveFileName[currentSaveFile] + ".xml", xmlWriterSettings);
        writer.WriteStartDocument();
        writer.WriteStartElement("Data");

        WriteXmlString(writer, "Name", _name);
        WriteXmlFloat(writer, "Pressed", _numberSavePressed);
        WriteXmlFloat(writer, "Time", _timePlayed.Timed());

        Affichage.text = $"Nom du joueur : Romain \nTemp de jeu : {_timePlayed.Timed()} \nNombre de foit appuyer sur SAVE : {_numberSavePressed}";

        writer.WriteEndElement();
        writer.WriteEndDocument();
        writer.Close();
    }


    static void WriteXmlString(XmlWriter _writer, string _key, string _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteString(_value);
        _writer.WriteEndElement();
    }

    static void WriteXmlFloat(XmlWriter _writer, string _key, float _value)
    {
        _writer.WriteStartElement(_key);
        _writer.WriteValue(_value);
        _writer.WriteEndElement();
    }

    public static void LoadGame()
    {
        XmlDocument saveFile = new XmlDocument();
        if (!System.IO.File.Exists("C:/Users/romai/Documents/Unity/TP_XML_RomainBRAUD/XML_RomainBRAUD/Assets/Scripts/File1/" + "/" + saveFileName[currentSaveFile] + ".xml"))
        {
            //NoSaveFound();
            return;
        }
        saveFile.LoadXml(System.IO.File.ReadAllText("C:/Users/romai/Documents/Unity/TP_XML_RomainBRAUD/XML_RomainBRAUD/Assets/Scripts/File1/" + "/" + saveFileName[currentSaveFile] + ".xml"));

        string key;
        string value;
        foreach (XmlNode node in saveFile.ChildNodes[1]) { 
            key = node.Name;
            value = node.InnerText;
            
            switch(key)
            {
                case "Name":
                    _name = value;
                    break;
                case "Pressed":
                    _numberSavePressed = int.Parse(value);
                    break;
                case "Time":
                    _timePlayed.TimePlaying = float.Parse(value, CultureInfo.InvariantCulture);
                    break;
            }
        }
    }
}
