using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ChannelListManager.ViewModels
{
	internal abstract class ViewModelBase<TViewModel> : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void OnPropertyChangedForAllProperties()
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
		}

		protected void OnPropertyChanged<TProperty>(Expression<Func<TViewModel, TProperty>> propertyProjection)
		{
			string propertyName = PropertyName(propertyProjection);
			OnPropertyChanged(propertyName);
		}

		protected void SetProperty<TProperty>(ref TProperty field, TProperty value, Expression<Func<TViewModel, TProperty>> expression)
		{
			if (field == null)
			{
				if (value == null)
					return;
			}
			else
			{
				if (field.Equals(value))
					return;
			}

			field = value;
			OnPropertyChanged(expression);
		}

		protected void SetProperty<TProperty>(ref TProperty field, TProperty value, [CallerMemberName] string propertyName = "")
		{
			if (field == null)
			{
				if (value == null)
					return;
			}
			else
			{
				if (field.Equals(value))
					return;
			}

			field = value;
			OnPropertyChanged(propertyName);
		}

		protected void SetProperty<TProperty>(TProperty field, TProperty value, Action setValue, [CallerMemberName] string propertyName = "")
		{
			if (field == null)
			{
				if (value == null)
					return;
			}
			else
			{
				if (field.Equals(value))
					return;
			}

			setValue();
			OnPropertyChanged(propertyName);
		}

		#endregion

		protected static string PropertyName<TProperty>(Expression<Func<TViewModel, TProperty>> projection)
		{
			return ((MemberExpression)projection.Body).Member.Name;
		}
	}
}
