using System;

namespace CavemanTools
{
    /// <summary>
    /// Marker for Di Container registration by convetions to skip types annotated with it. 
    /// This is not automatic, you have to tell the container to ignore these types. 
    /// Useful when you want some types to be named according to some convention but to configure it differentely in the Container
    /// </summary>
    public class ManualContainerConfigAttribute : Attribute
    {

    }
}