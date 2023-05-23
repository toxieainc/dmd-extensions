﻿using System;
using System.Reactive;
using System.Reactive.Subjects;
using LibDmd.Frame;
using LibDmd.Input;

namespace LibDmd.Test.Stubs
{
	public class TestSource : IGray2Source
	{
		public string Name => "Test Source";

		public BehaviorSubject<Dimensions> Dimensions { get; set; } =
			new BehaviorSubject<Dimensions>(new Dimensions(128, 32));

		public IObservable<Unit> OnResume => null;
		public IObservable<Unit> OnPause => null;

		private readonly Subject<DmdFrame> _frames = new Subject<DmdFrame>();

		public IObservable<DmdFrame> GetGray2Frames() => _frames;

		public void AddFrame(DmdFrame frame)
		{
			Dimensions.OnNext(frame.Dimensions);
			_frames.OnNext(frame);
		}
	}
}