// MvxAppCompatActivity.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Droid.Views;
using Cirrious.MvvmCross.ViewModels;

namespace CrossCompat.Android.Infrastructure
{
	public abstract class MvxAppCompatActivity
		: MvxEventSourceAppCompatActivity
	, IMvxAndroidView
	{
		protected MvxAppCompatActivity()
		{
			BindingContext = new MvxAndroidBindingContext(this, this);
			this.AddEventListeners();
		}

		protected MvxAppCompatActivity(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{
			BindingContext = new MvxAndroidBindingContext(this, this);
			this.AddEventListeners();
		}

		public object DataContext
		{
			get { return BindingContext.DataContext; }
			set { BindingContext.DataContext = value; }
		}

		public IMvxViewModel ViewModel
		{
			get { return DataContext as IMvxViewModel; }
			set
			{
				DataContext = value;
				OnViewModelSet();
			}
		}

		public void MvxInternalStartActivityForResult(Intent intent, int requestCode)
		{
			StartActivityForResult(intent, requestCode);
		}

		protected virtual void OnViewModelSet()
		{
		}

		public IMvxBindingContext BindingContext { get; set; }

		public override void SetContentView(int layoutResId)
		{
			var view = this.BindingInflate(layoutResId, null);
			SetContentView(view);
		}
	}

	public abstract class MvxAppCompatActivity<TViewModel>
		: MvxAppCompatActivity
	, IMvxAndroidView<TViewModel> where TViewModel : class, IMvxViewModel
	{
		public new TViewModel ViewModel
		{
			get { return (TViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}
	}
}