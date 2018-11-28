using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MVCWebMockup
{
    public class ElementModel
    {
        private ArrayList elementList = new ArrayList();
        private ElementController controller;
        public ElementModel(ElementController controller)
        {
            this.controller = controller;
        }

        public ArrayList ElementList
        {
            get
            {
                return elementList;
            }
        }

        public AnyElement getElement(int index)
        {
            return (AnyElement)elementList[index];
        }
        /// <summary>
        /// add element
        /// </summary>
        public void AddElement(AnyElement element)
        {
            elementList.Add(element);
            UpdateViews();
        }
        /// <summary>
        /// update element
        /// </summary>
        public void UpdateShape(AnyElement element)
        {
            UpdateViews();
        }

        /// <summary>
        /// delete element
        /// </summary>
        public void DeleteShape(AnyElement element)
        {
            elementList.Remove(element);
            UpdateViews();
        }
        /// <summary>
        /// delete element
        /// </summary>
        public void DeleteShape(int index)
        {
            if (index >= 0 && index < elementList.Count)
            {
                elementList.RemoveAt(index);
                UpdateViews();
            }
        }

        public void UpdateViews()
        {
            controller.UpdateViews();
        }
    }
}
