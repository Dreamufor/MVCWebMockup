using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWebMockup
{
    public class ElementController
    {
        private ArrayList viewList;

        public ElementController()
        {
            viewList = new ArrayList();
        }
        /// <summary>
        /// Add view
        /// </summary>
        public IElementView AddView(IElementView view)
        {
            viewList.Add(view);
            return view;
        }
        /// <summary>
        /// Update view
        /// </summary>
        public void UpdateViews()
        {
            foreach (IElementView v in viewList)
            {
                v.RefreshView();
            }
        }
    }
}
