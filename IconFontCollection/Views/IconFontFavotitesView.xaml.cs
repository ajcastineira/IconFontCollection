﻿#region Version info.
/**
*	@file IconFontFavotitesView.xaml.cs
*	@brief Provides the page for displaying IconFonts registered favotites.
*
*	@par Version
*	1.2.4
*	@par Author
*	Nia Tomonaka
*	@par Copyright
*	Copyright (C) 2016 Chronoir.net
*	@par Created date
*	2016/10/23
*	@par Last update date
*	2016/12/07
*	@par Licence
*	BSD Licence（ 2-caluse ）
*	@par Contact
*	@@nia_tn1012（ https://twitter.com/nia_tn1012/ ）
*	@par Homepage
*	- http://chronoir.net/ ( Homepage )
*	- https://github.com/Nia-TN1012/IconFontCollection ( GitHub )
*/
#endregion

using System.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

/// <summary>
///		<see cref="IconFontCollection"/> namespace
/// </summary>
namespace IconFontCollection {
	/// <summary>
	///		Provides the page for displaying IconFonts registered favotites.
	/// </summary>
	public sealed partial class IconFontFavotitesView : Page {

		/// <summary>
		///		Creates a new instance of the <see cref="IconFontFavotitesView"/> class.
		/// </summary>
		public IconFontFavotitesView() {
			InitializeComponent();
		}

		/// <summary>
		///		Invoked when the transition from another page.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected override void OnNavigatedTo( NavigationEventArgs e ) {
			base.OnNavigatedTo( e );

			// Get the current View, to display the Back button, and stores the events of when you press the button.
			var currentView = SystemNavigationManager.GetForCurrentView();
			currentView.AppViewBackButtonVisibility =
				Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
			currentView.BackRequested += Page_BackRequested;
		}

		/// <summary>
		///		Invoked when the transition to another page.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected override void OnNavigatingFrom( NavigatingCancelEventArgs e ) {
			base.OnNavigatingFrom( e );

			// When this page go back to the caller, delete the event handler.
			if( e.NavigationMode == NavigationMode.Back ) {
				var currentView = SystemNavigationManager.GetForCurrentView();
				currentView.BackRequested -= Page_BackRequested;
				iconFontFavoritesViewModel.UnsubscribeAllEvents();
			}
		}

		/// <summary>
		///		Invoked when the Back button is pressed.
		/// </summary>
		private void Page_BackRequested( object sender, BackRequestedEventArgs e ) {
			if( Frame.CanGoBack ) {
				Frame.GoBack();
				e.Handled = true;
			}
		}

		/// <summary>
		///		Invoked when user press the Copy to the clipboard button.
		/// </summary>
		private void CopyToClipboardButton_Click( object sender, RoutedEventArgs e ) {
			var button = sender as Button;
			var dataPack = new DataPackage();
			var tag = button.Tag;
			if( tag != null ) {
				// Get character code from the Tag on the Button that user clicked.
				dataPack.SetText( tag.ToString() );
				Clipboard.SetContent( dataPack );
			}
		}

		/// <summary>
		///		Jumps to the first item in the <see cref="GridView"/>.
		/// </summary>
		public void GridViewJumpToFirstItem() {
			if( IconFontCollectionGridView.Items?.Any() ?? false ) {
				IconFontCollectionGridView.SelectedIndex = 0;
				IconFontCollectionGridView.ScrollIntoView( IconFontCollectionGridView.Items[0] );
			}
		}
	}
}
