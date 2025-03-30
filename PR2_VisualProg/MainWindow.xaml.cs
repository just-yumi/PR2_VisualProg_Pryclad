using System;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace PR2_VisualProg
{
    public partial class MainWindow : Window
    {
        private RotateTransform3D? myYRotate1;
        private RotateTransform3D? myXRotate1;
        private RotateTransform3D? myYRotate2;
        private RotateTransform3D? myXRotate2;

        private AxisAngleRotation3D? myYAxis1;
        private AxisAngleRotation3D? myXAxis1;
        private AxisAngleRotation3D? myYAxis2;
        private AxisAngleRotation3D? myXAxis2;

        private Transform3DGroup? myTransform1;
        private Transform3DGroup? myTransform2;

        private DispatcherTimer? MyTimer;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize rotation transforms for Model1
            myYAxis1 = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 40);
            myYRotate1 = new RotateTransform3D(myYAxis1);

            myXAxis1 = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            myXRotate1 = new RotateTransform3D(myXAxis1);

            // Initialize rotation transforms for Model2
            myYAxis2 = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            myYRotate2 = new RotateTransform3D(myYAxis2);

            myXAxis2 = new AxisAngleRotation3D(new Vector3D(0, 0, 1), -10);
            myXRotate2 = new RotateTransform3D(myXAxis2);

            // Setup transform groups
            myTransform1 = new Transform3DGroup();
            myTransform1.Children.Add(myYRotate1);
            myTransform1.Children.Add(myXRotate1);

            myTransform2 = new Transform3DGroup();
            myTransform2.Children.Add(myYRotate2);
            myTransform2.Children.Add(myXRotate2);

            // Apply transforms to models
            if (MyModel1 != null) MyModel1.Transform = myTransform1;
            if (MyModel2 != null) MyModel2.Transform = myTransform2;

            // Initialize timer
            MyTimer = new DispatcherTimer();
            MyTimer.Tick += MyTimer_Tick;
            MyTimer.Interval = TimeSpan.FromMilliseconds(50); // 50ms for smooth animation
        }

        private void MyTimer_Tick(object? sender, EventArgs e)
        {
            // Auto-rotate Model1 around Y axis
            if (myYAxis1 != null)
            {
                myYAxis1.Angle += 1;
            }

            // Auto-rotate Model2 around X axis
            if (myXAxis2 != null)
            {
                myXAxis2.Angle -= 1;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (myYAxis1 == null) return;
            myYAxis1.Angle += 7;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (myXAxis2 == null) return;
            myXAxis2.Angle -= 7;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (myXAxis1 == null) return;
            myXAxis1.Angle += 7;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            MyTimer?.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MyTimer?.Stop();
        }

        protected override void OnClosed(EventArgs e)
        {
            MyTimer?.Stop();
            base.OnClosed(e);
        }
    }
}