using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ElementNameSpace {
    public enum ElementType {
        Fire,
        Electric,
        Wind,
        Earth,
        Water
    }
    
    public class Elements {
        public static string GetElementWord(ElementType element){
            switch(element){
                case ElementType.Fire:
                    return "Ignisob";
                case ElementType.Electric:
                    return "Fulmenta";
                case ElementType.Wind:
                    return "Ventush";
                case ElementType.Earth:
                    return "Terrack";
                case ElementType.Water:
                    return "Aquapy";
            }
            return "";
        }
    }
}

