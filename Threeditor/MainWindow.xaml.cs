using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Threeditor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public bool isCameraMove = false;
        public Point lastCameraDragPosition;
        public ModelVisual3D selectedMesh;

        public MainWindow()
        {
            InitializeComponent();

            selectedMesh = startSelectedMesh;
        
        }

        private void StartMoveCameraHandler(object sender, MouseButtonEventArgs e)
        {
            /*if (e.MiddleButton == MouseButtonState.Pressed) {
                isCameraMove = true;
            }*/
        }

        private void StopMoveCameraHandler(object sender, MouseButtonEventArgs e)
        {
            // isCameraMove = false;
        }

        private void MoveCameraHandler(object sender, MouseEventArgs e)
        {
            
            /*if (isCameraMove)
            {

            }*/
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                /*double delta = 0.01;
                if (e.GetPosition(space).X - lastCameraDragPosition.X < 0)
                {
                    delta *= -1;
                }
                mainCamera.LookDirection = new Vector3D(mainCamera.LookDirection.X + delta, 0, -1);
                lastCameraDragPosition = e.GetPosition(space);*/
                double deltaX = 0;
                double deltaY = 0;
                if (e.GetPosition(space).X - lastCameraDragPosition.X < 0)
                {
                    deltaX = -0.005;
                }
                else if (e.GetPosition(space).X - lastCameraDragPosition.X > 0)
                {
                    deltaX = 0.005;
                }
                if (e.GetPosition(space).Y - lastCameraDragPosition.Y < 0)
                {
                    deltaY = 0.005;
                } else if (e.GetPosition(space).Y - lastCameraDragPosition.Y > 0)
                {
                    deltaY = -0.005;
                }
                mainCamera.LookDirection = new Vector3D(mainCamera.LookDirection.X + deltaX, mainCamera.LookDirection.Y + deltaY, -1);
                lastCameraDragPosition = e.GetPosition(space);
            }
        }

        private void SetXLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                TranslateTransform3D currentMeshTransformTranslate = ((TranslateTransform3D)(currentMeshTransform.Children[0]));
                currentMeshTransformTranslate.OffsetX = settedPropertyValue;
            }
        }

        private void SetYLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                TranslateTransform3D currentMeshTransformTranslate = ((TranslateTransform3D)(currentMeshTransform.Children[0]));
                currentMeshTransformTranslate.OffsetY = settedPropertyValue;
            }
        }

        private void SetZLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                TranslateTransform3D currentMeshTransformTranslate = ((TranslateTransform3D)(currentMeshTransform.Children[0]));
                currentMeshTransformTranslate.OffsetZ = settedPropertyValue;
            }
        }

        private void SetXRotationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                RotateTransform3D currentMeshTransformRotation = ((RotateTransform3D)(currentMeshTransform.Children[2]));
                currentMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 15);
            }
        }

        private void SetYRotationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                RotateTransform3D currentMeshTransformRotation = ((RotateTransform3D)(currentMeshTransform.Children[2]));
                currentMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 15);
            }
        }

        private void SetZRotationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                RotateTransform3D currentMeshTransformRotation = ((RotateTransform3D)(currentMeshTransform.Children[2]));
                currentMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 15);
            }
        }

        private void SetXScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                ScaleTransform3D currentMeshTransformScale = ((ScaleTransform3D)(currentMeshTransform.Children[1]));
                currentMeshTransformScale.ScaleY = settedPropertyValue;
            }
        }

        private void SetYScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                ScaleTransform3D currentMeshTransformScale = ((ScaleTransform3D)(currentMeshTransform.Children[1]));
                currentMeshTransformScale.ScaleY = settedPropertyValue;
            }
        }

        private void SetZScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
                Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
                ScaleTransform3D currentMeshTransformScale = ((ScaleTransform3D)(currentMeshTransform.Children[1]));
                currentMeshTransformScale.ScaleZ = settedPropertyValue;
            }
        }
    }
}
