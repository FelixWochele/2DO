using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _2DO_Client.ViewModels
{
    internal class SubmoduleSelector : DataTemplateSelector
    {

		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var contentControl = (container as FrameworkElement);

			if (item is ListSelectorViewModel)
				return contentControl.FindResource("listSelectorViewTemplate") as DataTemplate;

			if (item is CategorieSelectorViewModel)
				return contentControl.FindResource("categorieSelectorViewTemplate") as DataTemplate;

			return null;
		}
	}
}
