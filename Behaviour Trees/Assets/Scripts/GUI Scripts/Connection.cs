using System;
using UnityEditor;
using UnityEngine;
using System.Xml.Serialization;

public class Connection
{
   public ConnectionPoint inPoint;
   public ConnectionPoint outPoint;
   [XmlIgnore]public Action<Connection> OnClickRemoveConnection;

   
   public Connection() {}
   public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection) 
   {
       this.inPoint = inPoint;
       this.outPoint = outPoint;
       this.OnClickRemoveConnection = OnClickRemoveConnection;
   }


   public void Draw() {
       Handles.DrawLine(
           inPoint.rect.center,
           outPoint.rect.center
       );

       if( Handles.Button((inPoint.rect.center + outPoint.rect.center) *0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap) ) 
       {
           if(OnClickRemoveConnection != null)
           {
               OnClickRemoveConnection(this);
           }
       }//if
   }//end of draw


}//end of class
