﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using LibDmd.Input.ScreenGrabber;

namespace LibDmd.Input.Media
{
	public class ImageSource : IFrameSource
	{
		public string Name { get; } = "Image Source";

		public IObservable<Unit> OnResume => _onResume;
		public IObservable<Unit> OnPause => _onPause;

		private readonly ISubject<Unit> _onResume = new Subject<Unit>();
		private readonly ISubject<Unit> _onPause = new Subject<Unit>();

		private readonly BehaviorSubject<BitmapSource> _frames;

		public ImageSource(string fileName)
		{
			if (!File.Exists(fileName)) {
				throw new FileNotFoundException("Cannot find file \"" + fileName + "\".");
			}

			var bmp = new BitmapImage();
			bmp.BeginInit();
			bmp.UriSource = new Uri(fileName);
			bmp.EndInit();

			_frames = new BehaviorSubject<BitmapSource>(bmp);
		}

		public IObservable<BitmapSource> GetFrames()
		{
			return _frames;
		}
	}
}
