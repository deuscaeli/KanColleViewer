﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Models;
using Grabacr07.KanColleWrapper;
using Livet;

namespace Grabacr07.KanColleViewer.ViewModels
{
	public class StartContentViewModel : ViewModel
	{
		#region singleton

		private static readonly StartContentViewModel instance = new StartContentViewModel();

		public static StartContentViewModel Instance
		{
			get { return instance; }
		}

		#endregion

		#region CanDeleteInternetCache 変更通知プロパティ

		private bool _CanDeleteInternetCache = true;

		public bool CanDeleteInternetCache
		{
			get { return this._CanDeleteInternetCache; }
			set
			{
				if (this._CanDeleteInternetCache != value)
				{
					this._CanDeleteInternetCache = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region DeleteInternetCacheButtonContent 変更通知プロパティ

		private string _DeleteInternetCacheButtonContent = Properties.Resources.StartContent_ClearCacheButton;

		public string DeleteInternetCacheButtonContent
		{
			get { return this._DeleteInternetCacheButtonContent; }
			set
			{
				if (this._DeleteInternetCacheButtonContent != value)
				{
					this._DeleteInternetCacheButtonContent = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region CanSetRegionCookie 変更通知プロパティ

		private bool _CanSetRegionCookie = true;

		public bool CanSetRegionCookie
		{
			get { return this._CanSetRegionCookie; }
			set
			{
				if (this._CanSetRegionCookie != value)
				{
					this._CanSetRegionCookie = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		#region SetRegionCookieButtonContent 変更通知プロパティ
		private string _SetRegionCookieButtonContent = Properties.Resources.StartContent_SetRegionCookieButton;

		public string SetRegionCookieButtonContent
		{
			get { return this._SetRegionCookieButtonContent; }
			set
			{
				if (this._SetRegionCookieButtonContent != value)
				{
					this._SetRegionCookieButtonContent = value;
					this.RaisePropertyChanged();
				}
			}
		}
		#endregion

		private StartContentViewModel() { }

		public async void DeleteInternetCache()
		{
			this.CanDeleteInternetCache = false;

			try
			{
				// ToDo: ダイアログで処理通知とか結果通知とかした方がよい
				// 今はとりあえず版ということで、1 回やったらボタンを非活性化しよう
				var result = await Helper.DeleteInternetCache();
				if (result)
				{
					this.DeleteInternetCacheButtonContent = Properties.Resources.StartContent_ClearCacheButtonMessage;
				}
				else
				{
					this.CanDeleteInternetCache = true;
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}

		public void SetRegionCookie()
		{
			App.ViewModelRoot.Navigator.CookieNavigate();
			this.SetRegionCookieButtonContent = Properties.Resources.StartContent_SetRegionCookieMessage;
			this.CanSetRegionCookie = false;
		}

		public void GetLatestKCV()
		{
			Process.Start(KanColleClient.Current.Updater.GetOnlineVersion(0, true));
		}
	}
}
