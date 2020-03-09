using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace InfoGraph.Controls
{
    public class CoordAxesControl : SKCanvasView
    {
        private float _absoluteMinXOfCoord;
        private float _absoluteMaxXOfCoord;
        private float _absoluteMinYOfCoord;
        private float _absoluteMaxYOfCoord;
        private SKPaint _blackPaint;


        #region DependencyProperties

        public static readonly BindableProperty PointCoordinatesProperty = 
                    BindableProperty.Create(propertyName: nameof(PointCoordinates),
                        returnType: typeof(IEnumerable<Tuple<int,int>>),
                        defaultValue: new List<Tuple<int,int>>(),
                        declaringType: typeof(CoordAxesControl),
                        propertyChanged: DependencyPropertiesChanged);

        public IEnumerable<Tuple<int,int>> PointCoordinates
        {
            get { return _pointCoordinates; }
            set { _pointCoordinates = value; }
        }
        private IEnumerable<Tuple<int, int>> _pointCoordinates;

        #endregion

        #region methods
        private static void DependencyPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CoordAxesControl)bindable;

            // Avoid unnecessary invalidation
            if (oldValue != newValue)
            {
                //TODO check why binding is not set in control
                control.PointCoordinates = (List<Tuple<int, int>>)newValue;
                control.InvalidateSurface();
            }
        }

        
        /// <summary>
        /// Called method when canvas is drawn
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            if (PointCoordinates == null)
                return;

            var canvas = e.Surface.Canvas;
            canvas.Clear();

            _blackPaint = new SKPaint();
            _blackPaint.Color = SKColors.Black;
            _blackPaint.StrokeWidth = 5;
            var size = CanvasSize;

            _absoluteMinXOfCoord = 50;
            _absoluteMaxXOfCoord = size.Width - 50;

            _absoluteMinYOfCoord = size.Height - 50;
            _absoluteMaxYOfCoord = 50;


            var pointZero = new SKPoint(_absoluteMinXOfCoord, _absoluteMinYOfCoord);
            var pointMinXMaxY = new SKPoint(_absoluteMinXOfCoord, _absoluteMaxYOfCoord);
            var pointMaxXMinY = new SKPoint(_absoluteMaxXOfCoord, _absoluteMinYOfCoord);

            DrawAxes(size, canvas, pointZero, pointMinXMaxY, pointMaxXMinY);
            DrawGraph(canvas);
        }

        private void DrawAxes(SKSize size, SKCanvas canvas, SKPoint pointZero, SKPoint pointMinXMaxY, SKPoint pointMaxXMinY)
        {
            //draw coordinate axes
            canvas.DrawLine(pointZero, pointMinXMaxY, _blackPaint);
            canvas.DrawLine(pointZero, pointMaxXMinY, _blackPaint);

            //draw coordinate arcs
            //MaxY
            canvas.DrawLine(pointMinXMaxY, new SKPoint(25, 75), _blackPaint);
            canvas.DrawLine(pointMinXMaxY, new SKPoint(75, 75), _blackPaint);

            //MaxX
            canvas.DrawLine(pointMaxXMinY, new SKPoint(size.Width - 75, size.Height - 75), _blackPaint);
            canvas.DrawLine(pointMaxXMinY, new SKPoint(size.Width - 75, size.Height - 25), _blackPaint);

        }

        protected void DrawGraph(SKCanvas canvas)
        {
            if (PointCoordinates == null || PointCoordinates.Count() < 2)
            {
                return;
            }

            int maxYOfValues = PointCoordinates.Select(x => x.Item2).Max();
            int maxXOfValues = PointCoordinates.Select(x => x.Item1).Max();
            float distanceOfStepsY = (_absoluteMinYOfCoord - _absoluteMaxYOfCoord) / maxYOfValues;
            float distanceOfStepsX = (_absoluteMaxXOfCoord - _absoluteMinXOfCoord) / maxXOfValues;

            for (int relativeX = 0; relativeX < PointCoordinates.Count()-1; relativeX++)
            {
                var firstCoord = PointCoordinates.ElementAt(relativeX);
                var secondCoord = PointCoordinates.ElementAt(relativeX + 1);

                float absoluteXFirstPoint = (firstCoord.Item1 * distanceOfStepsX) + _absoluteMinXOfCoord;
                float absoluteYFirstPoint = _absoluteMinYOfCoord - (firstCoord.Item2 * distanceOfStepsY) ;
                var firstPoint = new SKPoint(absoluteXFirstPoint, absoluteYFirstPoint);

                float absoluteXSecondPoint = (secondCoord.Item1 * distanceOfStepsX) + _absoluteMinXOfCoord;
                float absoluteYSecondPoint = _absoluteMinYOfCoord - (secondCoord.Item2 * distanceOfStepsY);
                var secondPoint = new SKPoint(absoluteXSecondPoint, absoluteYSecondPoint);

                canvas.DrawLine(firstPoint, secondPoint, _blackPaint);
            }
        }
        #endregion
    }
}
