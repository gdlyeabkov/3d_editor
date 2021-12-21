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
using System.Speech.Synthesis;
using System.IO;

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
        public SpeechSynthesizer debugger;
        public List<Dictionary<String, Object>> meshs;


        public MainWindow()
        {
            InitializeComponent();

            selectedMesh = startSelectedMesh;
            debugger = new SpeechSynthesizer();

            MeshGeometry3D mg3 = ((MeshGeometry3D)(((GeometryModel3D)(startSelectedMesh.Content)).Geometry));
            for (int index = 0; index < ((MeshGeometry3D)(((GeometryModel3D)(startSelectedMesh.Content)).Geometry)).TriangleIndices.Count; index += 3)
            {
                Polygon wireframe = new Polygon();

                /*wireframe.Points.Add(mg3.Positions[mg3.TriangleIndices[index]]);
                wireframe.Points.Add(mg3.Positions[mg3.TriangleIndices[index + 1]]);
                wireframe.Points.Add(mg3.Positions[mg3.TriangleIndices[index + 2]]);
                wireframe.Points.Add(mg3.Positions[mg3.TriangleIndices[index]]);*/

                wireframe.Fill = Brushes.LightBlue;
                wireframe.StrokeThickness = 1;

                canvas.Children.Add(wireframe);
            }

            meshs = new List<Dictionary<String, Object>>();
            Dictionary<String, Object>  defaultMesh = new Dictionary<String, Object>();
            defaultMesh.Add("index", ((int)(1)));
            defaultMesh.Add("name", "куб");
            defaultMesh.Add("parent", "none");
            meshs.Add(defaultMesh);
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

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                // parmeshs[currentMeshIdx]["name"];

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

        private void CreateCubeHandler(object sender, MouseButtonEventArgs e)
        {
            ModelVisual3D gameObjectMesh = new ModelVisual3D();
            MeshGeometry3D gameObjectMeshGeometry3D = new MeshGeometry3D();
            Point3DCollection gameObjectMeshPositions = new Point3DCollection();
            gameObjectMeshPositions.Add(new Point3D(0, 0, 0));
            gameObjectMeshPositions.Add(new Point3D(1, 0, 0));
            gameObjectMeshPositions.Add(new Point3D(1, 1, 0));
            gameObjectMeshPositions.Add(new Point3D(0, 1, 0));
            gameObjectMeshPositions.Add(new Point3D(0, 0, 1));
            gameObjectMeshPositions.Add(new Point3D(1, 0, 1));
            gameObjectMeshPositions.Add(new Point3D(1, 1, 1));
            gameObjectMeshPositions.Add(new Point3D(0, 1, 1));
            gameObjectMeshGeometry3D.Positions = gameObjectMeshPositions;
            Int32Collection gameObjectMeshTriangleIndices = new Int32Collection();
            gameObjectMeshTriangleIndices.Add(0);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(3);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(2);
            gameObjectMeshTriangleIndices.Add(3);
            gameObjectMeshTriangleIndices.Add(0);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(3);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(7);
            gameObjectMeshTriangleIndices.Add(3);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(6);
            gameObjectMeshTriangleIndices.Add(7);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(5);
            gameObjectMeshTriangleIndices.Add(6);
            gameObjectMeshTriangleIndices.Add(0);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(4);
            gameObjectMeshTriangleIndices.Add(5);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(2);
            gameObjectMeshTriangleIndices.Add(6);
            gameObjectMeshTriangleIndices.Add(6);
            gameObjectMeshTriangleIndices.Add(5);
            gameObjectMeshTriangleIndices.Add(1);
            gameObjectMeshTriangleIndices.Add(2);
            gameObjectMeshTriangleIndices.Add(3);
            gameObjectMeshTriangleIndices.Add(7);
            gameObjectMeshTriangleIndices.Add(7);
            gameObjectMeshTriangleIndices.Add(6);
            gameObjectMeshTriangleIndices.Add(2);
            gameObjectMeshGeometry3D.TriangleIndices = gameObjectMeshTriangleIndices;
            GeometryModel3D gameObjectMeshGeometryModel = new GeometryModel3D();
            gameObjectMeshGeometryModel.Geometry = gameObjectMeshGeometry3D;
            Transform3DGroup gameObjectMeshTransform = new Transform3DGroup();
            ScaleTransform3D gameObjectMeshTransformScale = new ScaleTransform3D();
            gameObjectMeshTransformScale.ScaleX = 0.1;
            gameObjectMeshTransformScale.ScaleY = 0.1;
            gameObjectMeshTransformScale.ScaleZ = 0.1;
            transformXScale.Text = gameObjectMeshTransformScale.ScaleX.ToString();
            transformYScale.Text = gameObjectMeshTransformScale.ScaleY.ToString();
            transformZScale.Text = gameObjectMeshTransformScale.ScaleZ.ToString();
            TranslateTransform3D gameObjectMeshTransformTranslate = new TranslateTransform3D();
            /*gameObjectMeshTransformTranslate.OffsetX = 3;
            gameObjectMeshTransformTranslate.OffsetY = 0;
            gameObjectMeshTransformTranslate.OffsetZ = 0;*/
            gameObjectMeshTransformTranslate.OffsetX =  Canvas.GetLeft(meshCursor);
            gameObjectMeshTransformTranslate.OffsetY = Canvas.GetTop(meshCursor);
            gameObjectMeshTransformTranslate.OffsetZ = 0;
            transformXLocation.Text = gameObjectMeshTransformTranslate.OffsetX.ToString();
            transformYLocation.Text = gameObjectMeshTransformTranslate.OffsetY.ToString();
            transformZLocation.Text = gameObjectMeshTransformTranslate.OffsetZ.ToString();
            RotateTransform3D gameObjectMeshTransformRotate = new RotateTransform3D();
            gameObjectMeshTransformRotate.Rotation = new AxisAngleRotation3D(new Vector3D(1, 1, 1), 45);
            transformXRotation.Text = ((AxisAngleRotation3D)(gameObjectMeshTransformRotate.Rotation)).Angle.ToString();
            transformYRotation.Text = ((AxisAngleRotation3D)(gameObjectMeshTransformRotate.Rotation)).Angle.ToString();
            transformZRotation.Text = ((AxisAngleRotation3D)(gameObjectMeshTransformRotate.Rotation)).Angle.ToString();
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformTranslate);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformScale);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformRotate);
            gameObjectMeshGeometryModel.Transform = gameObjectMeshTransform;
            MaterialGroup gameObjectMeshMaterialGroup = new MaterialGroup();
            DiffuseMaterial gameObjectMeshDiffuseMaterial = new DiffuseMaterial();
            Color gameObjectMeshSolidColor = new Color();
            gameObjectMeshSolidColor.R = 255;
            gameObjectMeshSolidColor.G = 0;
            gameObjectMeshSolidColor.B = 0;
            gameObjectMeshDiffuseMaterial.Brush = System.Windows.Media.Brushes.Red;
            gameObjectMeshMaterialGroup.Children.Add(gameObjectMeshDiffuseMaterial);
            gameObjectMeshGeometryModel.Material = gameObjectMeshMaterialGroup;
            gameObjectMesh.Content = gameObjectMeshGeometryModel;
            space.Children.Add(gameObjectMesh);

            selectedMesh = gameObjectMesh;
            foreach (StackPanel sceneCollectionItem in sceneCollection.Children)
            {
                sceneCollectionItem.Background = System.Windows.Media.Brushes.Transparent;
            }
            StackPanel newSceneCollectionItem = new StackPanel();
            newSceneCollectionItem.Orientation = Orientation.Horizontal;
            newSceneCollectionItem.Background = System.Windows.Media.Brushes.SkyBlue;
            TextBlock newSceneCollectionItemLabel = new TextBlock();
            newSceneCollectionItemLabel.Text = "🛆";
            newSceneCollectionItemLabel.Margin = new Thickness(25, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemLabel);
            TextBlock newSceneCollectionItemIcon = new TextBlock();
            newSceneCollectionItemIcon.Text = "Новый куб " + (space.Children.Count - 1).ToString();
            newSceneCollectionItemIcon.Margin = new Thickness(5, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemIcon);
            sceneCollection.Children.Insert(sceneCollection.Children.Count - 1, newSceneCollectionItem);
            newSceneCollectionItem.MouseUp += SelectMeshHandler;

            Dictionary<String, Object> newMesh = new Dictionary<String, Object>();
            newMesh.Add("index", ((int)(space.Children.IndexOf(gameObjectMesh))));
            newMesh.Add("name", newSceneCollectionItemIcon.Text);
            newMesh.Add("parent", "none");
            meshs.Add(newMesh);

        }

        private void SelectMeshHandler(object sender, MouseButtonEventArgs e)
        {
            StackPanel currentSceneCollectionItem = ((StackPanel)(sender));
            foreach (StackPanel sceneCollectionItem in sceneCollection.Children)
            {
                sceneCollectionItem.Background = System.Windows.Media.Brushes.Transparent;
            }
            currentSceneCollectionItem.Background = System.Windows.Media.Brushes.SkyBlue;
            selectedMesh = (ModelVisual3D)space.Children[sceneCollection.Children.IndexOf(currentSceneCollectionItem) - 1];
            Model3D currentMeshModel = ((Model3D)(selectedMesh.Content));
            Transform3DGroup currentMeshTransform = ((Transform3DGroup)(currentMeshModel.Transform));
            TranslateTransform3D currentMeshTransformTranslate = ((TranslateTransform3D)(currentMeshTransform.Children[0]));
            transformXLocation.Text = currentMeshTransformTranslate.OffsetX.ToString();
            transformYLocation.Text = currentMeshTransformTranslate.OffsetY.ToString();
            transformZLocation.Text = currentMeshTransformTranslate.OffsetZ.ToString();
            RotateTransform3D currentMeshTransformRotate = ((RotateTransform3D)(currentMeshTransform.Children[2]));
            transformXRotation.Text = ((AxisAngleRotation3D)(currentMeshTransformRotate.Rotation)).Angle.ToString();
            transformYRotation.Text = ((AxisAngleRotation3D)(currentMeshTransformRotate.Rotation)).Angle.ToString();
            transformZRotation.Text = ((AxisAngleRotation3D)(currentMeshTransformRotate.Rotation)).Angle.ToString();
            ScaleTransform3D currentMeshTransformScale = ((ScaleTransform3D)(currentMeshTransform.Children[1]));
            transformXScale.Text = currentMeshTransformScale.ScaleX.ToString();
            transformYScale.Text = currentMeshTransformScale.ScaleY.ToString();
            transformZScale.Text = currentMeshTransformScale.ScaleZ.ToString();
        }

        private void Set3DCursorLocationHandler(object sender, MouseButtonEventArgs e)
        {
            Canvas canvas = ((Canvas)(sender));
            Canvas.SetLeft(meshCursor, e.GetPosition(canvas).X - meshCursor.Width / 2);
            Canvas.SetTop(meshCursor, e.GetPosition(canvas).Y - meshCursor.Width / 2);
        }

        private void GlobalHotKeysHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A && ((Keyboard.Modifiers & ModifierKeys.Shift) > 0))
            {
                debugger.Speak("Меню создания");
                // ContextMenu contextMenu = new ContextMenu();
                /*ItemCollection spaceContextMenuCollection = spaceContextMenu.Items;
                foreach (MenuItem currentSpaceContextMenuItem in spaceContextMenuCollection)
                {
                    spaceContextMenu.Items.Remove(currentSpaceContextMenuItem);
                }
                MenuItem spaceContextMenuItem = new MenuItem();
                spaceContextMenuItem.Header = "Добавить";
                spaceContextMenu.Items.Add(spaceContextMenuItem);
                spaceContextMenu.SetValue(VisibilityProperty, Visibility.Visible);*/
            }
        }

        private void RenderHandler(object sender, RoutedEventArgs e)
        {
            int width = 512;
            int height = 512;
            var v = space as Viewport3D;
            /*v.Width = width;
            v.Height = height;*/
            v.Measure(new Size(width, height));
            v.Arrange(new Rect(0, 0, width, height));
            var vRect = new Rectangle();
            vRect.Width = width;
            vRect.Height = height;
            vRect.Fill = Brushes.White;
            vRect.Arrange(new Rect(0, 0, vRect.Width, vRect.Height));
            var bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            v.Dispatcher.Invoke(((Action)(() => bmp.Render(v))), System.Windows.Threading.DispatcherPriority.Render);
            bmp.Render(vRect);
            bmp.Render(v);

            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(bmp));

            var dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "PNG Images (.png)|*.png";
            if (dlg.ShowDialog() ?? false == true)
            {
                string filepath = dlg.FileName;
                using (var stm = File.Create(filepath))
                    png.Save(stm);
            }
        }

        private void ToggleTransformSettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (transformSettings.Visibility == Visibility.Visible)
            {
                transformSettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (transformSettings.Visibility == Visibility.Collapsed)
            {
                transformSettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

        private void ToggleRelationSettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (relationSettings.Visibility == Visibility.Visible)
            {
                relationSettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (relationSettings.Visibility == Visibility.Collapsed)
            {
                relationSettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

        private void SetPossibleParentHandler(object sender, EventArgs e)
        {
            ComboBox parentSelector = ((ComboBox)(sender));
            if (parentSelector.SelectedIndex != -1) { 
                debugger.Speak("Задаю связь");
                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                meshs[currentMeshIdx]["parent"] = ((ComboBoxItem)(parentSelector.Items[parentSelector.SelectedIndex])).Content.ToString();
                debugger.Speak(meshs[currentMeshIdx]["parent"].ToString());
            } else
            {
                debugger.Speak("Не могу задать связь");
            }
        }

        private void GetPossibleParentsHandler(object sender, EventArgs e)
        {
            ComboBox parentSelector = ((ComboBox)(sender));
            parentSelector.Items.Clear();
            foreach (StackPanel sceneCollectionItem in sceneCollection.Children)
            {
                ComboBoxItem possibleParent = new ComboBoxItem();
                possibleParent.Content = ((TextBlock)(sceneCollectionItem.Children[1])).Text;
                parentSelector.Items.Add(possibleParent);
            }
        }
    }
}
