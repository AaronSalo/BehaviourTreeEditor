using System;
using UnityEngine;
using System.Xml.Serialization;

public enum ConnectionPointType {IN, OUT}

public class ConnectionPoint
{
    public string id;
    [XmlIgnore] public Rect rect;

    [XmlIgnore]  public ConnectionPointType type;

    [XmlIgnore] public Node node;

    [XmlIgnore] public GUIStyle style;

    [XmlIgnore] public Action<ConnectionPoint> OnClickConnectionPoint;


    public ConnectionPoint() {}
    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint, 
    string id = null)
    {
        this.node = node;
        this.type = type;
        this.style = style;
        this.OnClickConnectionPoint = OnClickConnectionPoint;
        rect = new Rect(0, 0, 20f, 10f);
        this.id = id ?? Guid.NewGuid().ToString();
    }

    public void Draw() {
        //rect.y = node.rect.y + (node.rect.height *0.5f) - rect.height * 0.5f;
        rect.x = node.rect.x + node.rect.width/4;

        switch(type) {
            
            case ConnectionPointType.IN:
                //rect.x = node.rect.x - rect.width + 8f;
                rect.y = node.rect.y - 5f;
                break;

            case ConnectionPointType.OUT:
                //rect.x = node.rect.x + node.rect.width - 8f;
                rect.y = node.rect.y + node.rect.height - 8f;
                break;

        }//swtich

        if(GUI.Button(rect, "", style) ) {
            if(OnClickConnectionPoint != null ) {
                OnClickConnectionPoint(this);
            }
        }//if

    }//end of draw

}
