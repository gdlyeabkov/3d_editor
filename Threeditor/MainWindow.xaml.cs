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
using Microsoft.Win32;

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
            defaultMesh.Add("name", "Куб");
            defaultMesh.Add("parent", "none");
            defaultMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            defaultMesh.Add("normals", 45);
            defaultMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            defaultMesh.Add("type", "mesh");
            defaultMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
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
                List<Dictionary<String, Object>> childs = meshs.Where<Dictionary<String, Object>>((mesh) => mesh["parent"].ToString() == meshs[currentMeshIdx]["name"].ToString()).ToList<Dictionary<String, Object>>();
                foreach (Dictionary<String, Object> child in childs)
                {
                    foreach (ModelVisual3D mesh in space.Children)
                    {
                        if (space.Children.IndexOf(mesh) == ((int)(child["index"])))
                        {
                            Model3D model = ((Model3D)(mesh.Content));
                            Transform3DGroup transform = ((Transform3DGroup)(model.Transform));
                            TranslateTransform3D transformTranslate = ((TranslateTransform3D)(transform.Children[0]));
                            transformTranslate.OffsetX = settedPropertyValue + settedPropertyValue;
                        }
                    }
                }

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

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                List<Dictionary<String, Object>> childs = meshs.Where<Dictionary<String, Object>>((mesh) => mesh["parent"].ToString() == meshs[currentMeshIdx]["name"].ToString()).ToList<Dictionary<String, Object>>();
                foreach (Dictionary<String, Object> child in childs)
                {
                    foreach (ModelVisual3D mesh in space.Children)
                    {
                        if (space.Children.IndexOf(mesh) == ((int)(child["index"])))
                        {
                            Model3D model = ((Model3D)(mesh.Content));
                            Transform3DGroup transform = ((Transform3DGroup)(model.Transform));
                            TranslateTransform3D transformTranslate = ((TranslateTransform3D)(transform.Children[0]));
                            transformTranslate.OffsetY = settedPropertyValue + settedPropertyValue;
                        }
                    }
                }

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

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                List<Dictionary<String, Object>> childs = meshs.Where<Dictionary<String, Object>>((mesh) => mesh["parent"].ToString() == meshs[currentMeshIdx]["name"].ToString()).ToList<Dictionary<String, Object>>();
                foreach (Dictionary<String, Object> child in childs)
                {
                    foreach (ModelVisual3D mesh in space.Children)
                    {
                        if (space.Children.IndexOf(mesh) == ((int)(child["index"])))
                        {
                            Model3D model = ((Model3D)(mesh.Content));
                            Transform3DGroup transform = ((Transform3DGroup)(model.Transform));
                            TranslateTransform3D transformTranslate = ((TranslateTransform3D)(transform.Children[0]));
                            transformTranslate.OffsetZ = settedPropertyValue + settedPropertyValue;
                        }
                    }
                }

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
            gameObjectMeshTransformTranslate.OffsetX = ((canvas.Width / 2 - Canvas.GetLeft(meshCursor)) * -1) / 15;
            gameObjectMeshTransformTranslate.OffsetY = (canvas.Height - 100 - Canvas.GetTop(meshCursor)) / 20;
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
            SpecularMaterial gameObjectMeshSpecularMaterial = new SpecularMaterial();
            gameObjectMeshSpecularMaterial.SpecularPower = 150;
            gameObjectMeshSpecularMaterial.Brush = System.Windows.Media.Brushes.Green;
            gameObjectMeshMaterialGroup.Children.Add(gameObjectMeshSpecularMaterial);
            EmissiveMaterial gameObjectMeshEmissiveMaterial = new EmissiveMaterial();
            gameObjectMeshEmissiveMaterial.Brush = System.Windows.Media.Brushes.Green;
            gameObjectMeshMaterialGroup.Children.Add(gameObjectMeshEmissiveMaterial);
            DiffuseMaterial gameObjectMeshDiffuseMaterial = new DiffuseMaterial();
            gameObjectMeshMaterialGroup.Children.Add(gameObjectMeshDiffuseMaterial);
            Color gameObjectMeshSolidColor = new Color();
            gameObjectMeshSolidColor.R = 255;
            gameObjectMeshSolidColor.G = 0;
            gameObjectMeshSolidColor.B = 0;
            gameObjectMeshDiffuseMaterial.Brush = System.Windows.Media.Brushes.Red;
            /*ImageBrush imageBrush = new ImageBrush();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png", UriKind.Absolute);
            bitmapImage.EndInit();
            imageBrush.ImageSource = bitmapImage;
            gameObjectMeshDiffuseMaterial.Brush = imageBrush;*/
            
            

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
            newMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            newMesh.Add("normals", 45);
            newMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            newMesh.Add("type", "mesh");
            newMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
            meshs.Add(newMesh);

            transformSelectedMeshName.Text = newMesh["name"].ToString();
            ComboBoxItem selectableMesh = new ComboBoxItem();
            selectableMesh.Content = transformSelectedMeshName.Text;
            transformMeshSelector.Items.Insert(transformMeshSelector.Items.Count - 1, selectableMesh);
            transformMeshSelector.SelectedIndex = transformMeshSelector.Items.Count - 2;

            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;

            normalsAngle.Text = "45";

            textureXLocation.Text = "0";
            textureYLocation.Text = "1";
            textureZLocation.Text = "1";
            textureXScale.Text = "0";
            textureYScale.Text = "0";
            textureZScale.Text = "1";

            // debugger.Speak(((BitmapImage)(((ImageBrush)(gameObjectMeshDiffuseMaterial.Brush)).ImageSource)).UriSource.AbsoluteUri);

            ImageBrush imageBrush = new ImageBrush();
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(@"file:///C:/pictures/default_texture.png");
            bitmapImage.EndInit();
            imageBrush.ImageSource = bitmapImage;
            gameObjectMeshDiffuseMaterial.Brush = imageBrush;

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

            int currentMeshIdx = 0;
            foreach (Dictionary<String, Object> mesh in meshs)
            {
                if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                {
                    currentMeshIdx = meshs.IndexOf(mesh);
                }
            }
            transformSelectedMeshName.Text = meshs[currentMeshIdx]["name"].ToString();
            transformMeshSelector.SelectedIndex = ((int)(meshs[currentMeshIdx]["index"]));
            ComboBoxItem parent = null;
            foreach (ComboBoxItem possibleParent in relationParent.Items) {
                if (possibleParent.Content.ToString() == meshs[currentMeshIdx]["parent"])
                {
                    parent = possibleParent;
                }
            }
            relationParent.SelectedItem = ((ComboBoxItem)(parent));

            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;
            foreach (Dictionary<String, Object> modifier in ((List<Dictionary<String, Object>>)(meshs[currentMeshIdx]["modifiers"])))
            {
                StackPanel newModifier = new StackPanel();
                newModifier.Orientation = Orientation.Horizontal;
                newModifier.Margin = new Thickness(0, 5, 0, 5);
                TextBlock newModifierName = new TextBlock();
                newModifierName.Margin = new Thickness(5, 0, 5, 0);
                newModifierName.Text = modifier["name"].ToString();
                newModifier.Children.Add(newModifierName);
                modifiers.Children.Add(newModifier);
            }

            normalsAngle.Text = ((int)(meshs[currentMeshIdx]["normals"])).ToString();
            textureXLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[0].ToString();
            textureYLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[1].ToString();
            textureZLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[2].ToString();
            textureXScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[3].ToString();
            textureYScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[4].ToString();
            textureZScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[5].ToString();

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
                contextMenuRaiser.ContextMenu.IsOpen = true;
            }
            else if (e.Key == Key.L && ((Keyboard.Modifiers & ModifierKeys.Shift) > 0) && ((Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                CreateAmbientLight();
            }
            else if (e.Key == Key.L && ((Keyboard.Modifiers & ModifierKeys.Shift) > 0))
            {
                CreateDirectionalLight();
            }
            else if (e.Key == Key.L && ((Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                CreatePointLight();
            }
            else if (e.Key == Key.L)
            {
                CreateSpotLight();
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

            int currentMeshIdx = 0;
            foreach (Dictionary<String, Object> mesh in meshs)
            {
                if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                {
                    currentMeshIdx = meshs.IndexOf(mesh);
                }
            }

            foreach (StackPanel sceneCollectionItem in sceneCollection.Children)
            {
                ComboBoxItem possibleParent = new ComboBoxItem();
                possibleParent.Content = ((TextBlock)(sceneCollectionItem.Children[1])).Text;
                parentSelector.Items.Add(possibleParent);

                if (possibleParent.Content.ToString() == meshs[currentMeshIdx]["name"].ToString())
                {
                    possibleParent.IsEnabled = false;
                }

            }
        }

        private void ToggleTabHandler(object sender, MouseButtonEventArgs e)
        {
            TextBlock tab = ((TextBlock)(sender));
            int tabParam = Int32.Parse(tab.DataContext.ToString());
            tabs.SelectedIndex = tabParam;
        }

        private void ToggleDisplaySettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (displaySettings.Visibility == Visibility.Visible)
            {
                displaySettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (displaySettings.Visibility == Visibility.Collapsed)
            {
                displaySettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

        private void SetColorHandler(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key) {
                TextBox inputField = ((TextBox)(sender));
                string rawColor = inputField.Text;
                string[] colorChannels = rawColor.Split(new Char[] { ',' });
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MaterialGroup currentMeshMaterialGroup = ((MaterialGroup)(currentMeshModel.Material));
                DiffuseMaterial currentMeshMaterial = new DiffuseMaterial();
                /*Color colour = new Color();
                colour.R = Byte.Parse(colorChannels[0]);
                colour.G = Byte.Parse(colorChannels[1]);
                colour.B = Byte.Parse(colorChannels[2]);
                debugger.Speak(colorChannels[0]);
                debugger.Speak(colorChannels[1]);
                debugger.Speak(colorChannels[2]);
                currentMeshMaterial.Color = colour;*/
                currentMeshMaterial.Brush = Brushes.Black;
                currentMeshMaterialGroup.Children.Add(currentMeshMaterial);
            }
        }

        private void SelectMeshFromListHandler(object sender, EventArgs e)
        {
            ComboBox meshList = ((ComboBox)(sender));
            if (meshList.SelectedIndex >= 0) {
                StackPanel currentSceneCollectionItem = ((StackPanel)(sceneCollection.Children[meshList.SelectedIndex + 1]));
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

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                transformSelectedMeshName.Text = meshs[currentMeshIdx]["name"].ToString();
                transformMeshSelector.SelectedIndex = ((int)(meshs[currentMeshIdx]["index"]));
                ComboBoxItem parent = null;
                foreach (ComboBoxItem possibleParent in relationParent.Items)
                {
                    if (possibleParent.Content.ToString() == meshs[currentMeshIdx]["parent"])
                    {
                        parent = possibleParent;
                    }
                }
                relationParent.SelectedItem = ((ComboBoxItem)(parent));

                modifiers.Children.Clear();
                embeddedModifiers.SelectedIndex = 0;
                foreach (Dictionary<String, Object> modifier in ((List<Dictionary<String, Object>>)(meshs[currentMeshIdx]["modifiers"])))
                {
                    StackPanel newModifier = new StackPanel();
                    newModifier.Orientation = Orientation.Horizontal;
                    newModifier.Margin = new Thickness(0, 5, 0, 5);
                    TextBlock newModifierName = new TextBlock();
                    newModifierName.Margin = new Thickness(5, 0, 5, 0);
                    newModifierName.Text = modifier["name"].ToString();
                    newModifier.Children.Add(newModifierName);
                    modifiers.Children.Add(newModifier);
                }

                normalsAngle.Text = ((int)(meshs[currentMeshIdx]["normals"])).ToString();
                textureXLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[0].ToString();
                textureYLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[1].ToString();
                textureZLocation.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[2].ToString();
                textureXScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[3].ToString();
                textureYScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[4].ToString();
                textureZScale.Text = ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[5].ToString();

            }
        }

        private void AddModifierHandler(object sender, EventArgs e)
        {
            ComboBox modifiersList = ((ComboBox)(sender));
            
            int currentMeshIdx = 0;
            foreach (Dictionary<String, Object> mesh in meshs)
            {
                if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                {
                    currentMeshIdx = meshs.IndexOf(mesh);
                }
            }

            if (modifiersList.SelectedIndex >= 0 && ((ComboBoxItem)(modifiersList.Items[modifiersList.SelectedIndex])).IsEnabled)
            {
                StackPanel newModifier = new StackPanel();
                newModifier.Orientation = Orientation.Horizontal;
                newModifier.Margin = new Thickness(0, 5, 0, 5);
                TextBlock newModifierName = new TextBlock();
                newModifierName.Margin = new Thickness(5, 0, 5, 0);
                newModifierName.Text = ((ComboBoxItem)(modifiersList.Items[modifiersList.SelectedIndex])).Content.ToString();
                newModifier.Children.Add(newModifierName);
                modifiers.Children.Add(newModifier);

                Dictionary<String, Object> addedModifier = new Dictionary<String, Object>();
                addedModifier.Add("name", ((ComboBoxItem)(modifiersList.Items[modifiersList.SelectedIndex])).Content.ToString());
                ((List<Dictionary<String, Object>>)(meshs[currentMeshIdx]["modifiers"])).Add(addedModifier);

            }
            foreach (ComboBoxItem modifier in modifiersList.Items)
            {
                modifier.IsEnabled = true;
            }

            embeddedModifiers.SelectedIndex = 0;

        }

        private void GetModifiersHandler(object sender, EventArgs e)
        {
            ComboBox modifiersList = ((ComboBox)(sender));

            int currentMeshIdx = 0;
            foreach (Dictionary<String, Object> mesh in meshs)
            {
                if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                {
                    currentMeshIdx = meshs.IndexOf(mesh);
                }
            }

            foreach (ComboBoxItem modifier in modifiersList.Items)
            {
                if (((List<Dictionary<String, Object>>)(meshs[currentMeshIdx]["modifiers"])).Where<Dictionary<String, Object>>((possibleModifier) => possibleModifier["name"].ToString() == modifier.Content.ToString()).Count() >= 1)
                {
                    modifier.IsEnabled = false;
                } else
                {
                    modifier.IsEnabled = true;
                }
            }
        }

        private void SetNormalsAngleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                meshs[currentMeshIdx]["normals"] = Int32.Parse(normalsAngle.Text);

                bool isSmooth = ((bool)(normalsSmooth.IsChecked));
                if (isSmooth)
                {
                    meshs[currentMeshIdx]["normals"] = ((int)(((int)(meshs[currentMeshIdx]["normals"]))));
                }

                int currentNormals = ((int)(meshs[currentMeshIdx]["normals"]));
                Vector3DCollection normals = new Vector3DCollection();
                Vector3D normal = new Vector3D(currentNormals / 30 >= 1.0 ? 1 : 0, currentNormals / 60 >= 1.0 ? 1 : 0, currentNormals / 90 >= 1.0 ? 1 : 0);
                normals.Add(normal);
                normal = new Vector3D(currentNormals / 120 >= 1.0 ? 1 : 0, currentNormals / 150 >= 1.0 ? 1 : 0, currentNormals / 180 >= 1.0 ? 1 : 0);
                normals.Add(normal);
                normal = new Vector3D(currentNormals / 210 >= 1.0 ? 1 : 0, currentNormals / 240 >= 1.0 ? 1 : 0, currentNormals / 270 >= 1.0 ? 1 : 0);
                normals.Add(normal);
                normal = new Vector3D(currentNormals / 300 >= 1.0 ? 1 : 0, currentNormals / 330 >= 1.0 ? 1 : 0, currentNormals / 360 >= 1.0 ? 1 : 0);
                normals.Add(normal);
                ((MeshGeometry3D)(((GeometryModel3D)(selectedMesh.Content)).Geometry)).Normals = normals;

            }

        }

        private void SetNormalsSmoothHandler(object sender, KeyEventArgs e)
        {
            CheckBox checkbox = ((CheckBox)(sender));
            bool isSmooth = ((bool)(checkbox.IsChecked));
            int currentMeshIdx = 0;
            foreach (Dictionary<String, Object> mesh in meshs)
            {
                if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                {
                    currentMeshIdx = meshs.IndexOf(mesh);
                }
            }
            if (isSmooth)
            {
                meshs[currentMeshIdx]["normals"] = ((int)(((int)(meshs[currentMeshIdx]["normals"]))));
            }

            int currentNormals = ((int)(meshs[currentMeshIdx]["normals"]));
            Vector3DCollection normals = new Vector3DCollection();
            Vector3D normal = new Vector3D(currentNormals / 30 >= 1.0 ? 1 : 0, currentNormals / 60 >= 1.0 ? 1 : 0, currentNormals / 90 >= 1.0 ? 1 : 0);
            normals.Add(normal);
            normal = new Vector3D(currentNormals / 120 >= 1.0 ? 1 : 0, currentNormals / 150 >= 1.0 ? 1 : 0, currentNormals / 180 >= 1.0 ? 1 : 0);
            normals.Add(normal);
            normal = new Vector3D(currentNormals / 210 >= 1.0 ? 1 : 0, currentNormals / 240 >= 1.0 ? 1 : 0, currentNormals / 270 >= 1.0 ? 1 : 0);
            normals.Add(normal);
            normal = new Vector3D(currentNormals / 300 >= 1.0 ? 1 : 0, currentNormals / 330 >= 1.0 ? 1 : 0, currentNormals / 360 >= 1.0 ? 1 : 0);
            normals.Add(normal);
            ((MeshGeometry3D)(((GeometryModel3D)(selectedMesh.Content)).Geometry)).Normals = normals;

        }

        private void ToggleNormalsSettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (normalsSettings.Visibility == Visibility.Visible)
            {
                normalsSettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (transformSettings.Visibility == Visibility.Collapsed)
            {
                normalsSettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

        private void ToggleTexturesSettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (texturesSettings.Visibility == Visibility.Visible)
            {
                texturesSettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (transformSettings.Visibility == Visibility.Collapsed)
            {
                texturesSettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

        private void SetTextureXLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты положения по оси X");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(settedPropertyValue, currentMeshTransform.TextureCoordinates[0].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[1].X, currentMeshTransform.TextureCoordinates[1].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[2].X, currentMeshTransform.TextureCoordinates[2].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[3].X, currentMeshTransform.TextureCoordinates[3].Y));
                currentMeshTransform.TextureCoordinates = textures;

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[0] = settedPropertyValue;
            }
        }

        private void SetTextureYLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты положения по оси y");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[0].X, settedPropertyValue));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[1].X, currentMeshTransform.TextureCoordinates[1].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[2].X, currentMeshTransform.TextureCoordinates[2].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[3].X, currentMeshTransform.TextureCoordinates[3].Y));
                currentMeshTransform.TextureCoordinates = textures;
                
                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[1] = settedPropertyValue;
            
            }
        }

        private void SetTextureZLocationHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты положения по оси Z");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[0].X, currentMeshTransform.TextureCoordinates[0].Y));
                textures.Add(new Point(settedPropertyValue, settedPropertyValue));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[2].X, currentMeshTransform.TextureCoordinates[2].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[3].X, currentMeshTransform.TextureCoordinates[3].Y));
                currentMeshTransform.TextureCoordinates = textures;

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[2] = settedPropertyValue;

            }
        }

        private void SetTextureXScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты размера по оси x");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[0].X, currentMeshTransform.TextureCoordinates[0].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[1].X, currentMeshTransform.TextureCoordinates[1].Y));
                textures.Add(new Point(settedPropertyValue, currentMeshTransform.TextureCoordinates[2].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[3].X, currentMeshTransform.TextureCoordinates[3].Y));
                currentMeshTransform.TextureCoordinates = textures;

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[3] = settedPropertyValue;

            }
        }

        private void SetTextureYScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты размера по оси y");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[0].X, currentMeshTransform.TextureCoordinates[0].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[1].X, currentMeshTransform.TextureCoordinates[1].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[2].X, settedPropertyValue));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[3].X, currentMeshTransform.TextureCoordinates[3].Y));
                currentMeshTransform.TextureCoordinates = textures;

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[4] = settedPropertyValue;

            }
        }

        private void SetTextureZScaleHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                debugger.Speak("Задал текстурные координаты размера по оси z");
                TextBox settedProperty = ((TextBox)(sender));
                int settedPropertyValue = Int32.Parse(settedProperty.Text);
                GeometryModel3D currentMeshModel = ((GeometryModel3D)(selectedMesh.Content));
                MeshGeometry3D currentMeshTransform = ((MeshGeometry3D)(currentMeshModel.Geometry));
                PointCollection textures = new PointCollection();
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[0].X, currentMeshTransform.TextureCoordinates[0].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[1].X, currentMeshTransform.TextureCoordinates[1].Y));
                textures.Add(new Point(currentMeshTransform.TextureCoordinates[2].X, currentMeshTransform.TextureCoordinates[2].Y));
                textures.Add(new Point(settedPropertyValue, settedPropertyValue));
                currentMeshTransform.TextureCoordinates = textures;

                int currentMeshIdx = 0;
                foreach (Dictionary<String, Object> mesh in meshs)
                {
                    if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                    {
                        currentMeshIdx = meshs.IndexOf(mesh);
                    }
                }
                ((List<Int32>)(meshs[currentMeshIdx]["textures"]))[5] = settedPropertyValue;

            }
        }

        private void CreateDirectionalLight()
        {
            ModelVisual3D gameObjectMesh = new ModelVisual3D();
            DirectionalLight gameObjectMeshGeometryModel = new DirectionalLight();
            Transform3DGroup gameObjectMeshTransform = new Transform3DGroup();
            TranslateTransform3D gameObjectMeshTransformTranslate = new TranslateTransform3D();
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetY = 0;
            ScaleTransform3D gameObjectMeshTransformScale = new ScaleTransform3D();
            gameObjectMeshTransformScale.ScaleX = 0.1;
            gameObjectMeshTransformScale.ScaleY = 0.1;
            gameObjectMeshTransformScale.ScaleZ = 0.1;
            RotateTransform3D gameObjectMeshTransformRotation = new RotateTransform3D();
            gameObjectMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformTranslate);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformScale);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformRotation);
            gameObjectMeshGeometryModel.Transform = gameObjectMeshTransform;
            Color lightColor = new Color();
            lightColor.R = 255;
            lightColor.G = 0;
            lightColor.B = 0;
            gameObjectMeshGeometryModel.Color = lightColor;
            gameObjectMeshGeometryModel.Direction = new Vector3D(-0.612372, -0.5, -0.612372);
            transformXScale.Text = "1";
            transformYScale.Text = "1";
            transformZScale.Text = "1";
            transformXLocation.Text = "0";
            transformYLocation.Text = "0";
            transformZLocation.Text = "0";
            transformXRotation.Text = "0";
            transformYRotation.Text = "0";
            transformZRotation.Text = "0";
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
            newSceneCollectionItemIcon.Text = "Новый свет " + (space.Children.Count - 1).ToString();
            newSceneCollectionItemIcon.Margin = new Thickness(5, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemIcon);
            sceneCollection.Children.Insert(sceneCollection.Children.Count - 1, newSceneCollectionItem);
            newSceneCollectionItem.MouseUp += SelectMeshHandler;
            Dictionary<String, Object> newMesh = new Dictionary<String, Object>();
            newMesh.Add("index", ((int)(space.Children.IndexOf(gameObjectMesh))));
            newMesh.Add("name", newSceneCollectionItemIcon.Text);
            newMesh.Add("parent", "none");
            newMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            newMesh.Add("normals", 45);
            newMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            newMesh.Add("type", "directionalLight");
            newMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
            meshs.Add(newMesh);
            transformSelectedMeshName.Text = newMesh["name"].ToString();
            ComboBoxItem selectableMesh = new ComboBoxItem();
            selectableMesh.Content = transformSelectedMeshName.Text;
            transformMeshSelector.Items.Insert(transformMeshSelector.Items.Count - 1, selectableMesh);
            transformMeshSelector.SelectedIndex = transformMeshSelector.Items.Count - 2;
            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;
            normalsAngle.Text = "45";
            textureXLocation.Text = "0";
            textureYLocation.Text = "1";
            textureZLocation.Text = "1";
            textureXScale.Text = "0";
            textureYScale.Text = "0";
            textureZScale.Text = "1";
        }

        private void CreateAmbientLight()
        {
            ModelVisual3D gameObjectMesh = new ModelVisual3D();
            AmbientLight gameObjectMeshGeometryModel = new AmbientLight();
            Transform3DGroup gameObjectMeshTransform = new Transform3DGroup();
            TranslateTransform3D gameObjectMeshTransformTranslate = new TranslateTransform3D();
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetY = 0;
            ScaleTransform3D gameObjectMeshTransformScale = new ScaleTransform3D();
            gameObjectMeshTransformScale.ScaleX = 0.1;
            gameObjectMeshTransformScale.ScaleY = 0.1;
            gameObjectMeshTransformScale.ScaleZ = 0.1;
            RotateTransform3D gameObjectMeshTransformRotation = new RotateTransform3D();
            gameObjectMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformTranslate);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformScale);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformRotation);
            gameObjectMeshGeometryModel.Transform = gameObjectMeshTransform;
            Color lightColor = new Color();
            lightColor.R = 0;
            lightColor.G = 255;
            lightColor.B = 0;
            gameObjectMeshGeometryModel.Color = lightColor;
            transformXScale.Text = "1";
            transformYScale.Text = "1";
            transformZScale.Text = "1";
            transformXLocation.Text = "0";
            transformYLocation.Text = "0";
            transformZLocation.Text = "0";
            transformXRotation.Text = "0";
            transformYRotation.Text = "0";
            transformZRotation.Text = "0";
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
            newSceneCollectionItemIcon.Text = "Новый внешний свет " + (space.Children.Count - 1).ToString();
            newSceneCollectionItemIcon.Margin = new Thickness(5, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemIcon);
            sceneCollection.Children.Insert(sceneCollection.Children.Count - 1, newSceneCollectionItem);
            newSceneCollectionItem.MouseUp += SelectMeshHandler;
            Dictionary<String, Object> newMesh = new Dictionary<String, Object>();
            newMesh.Add("index", ((int)(space.Children.IndexOf(gameObjectMesh))));
            newMesh.Add("name", newSceneCollectionItemIcon.Text);
            newMesh.Add("parent", "none");
            newMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            newMesh.Add("normals", 45);
            newMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            newMesh.Add("type", "ambientLight");
            newMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
            meshs.Add(newMesh);
            transformSelectedMeshName.Text = newMesh["name"].ToString();
            ComboBoxItem selectableMesh = new ComboBoxItem();
            selectableMesh.Content = transformSelectedMeshName.Text;
            transformMeshSelector.Items.Insert(transformMeshSelector.Items.Count - 1, selectableMesh);
            transformMeshSelector.SelectedIndex = transformMeshSelector.Items.Count - 2;
            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;

            normalsAngle.Text = "45";

            textureXLocation.Text = "0";
            textureYLocation.Text = "1";
            textureZLocation.Text = "1";
            textureXScale.Text = "0";
            textureYScale.Text = "0";
            textureZScale.Text = "1";
        }

        private void CreatePointLight()
        {
            ModelVisual3D gameObjectMesh = new ModelVisual3D();
            PointLight gameObjectMeshGeometryModel = new PointLight();
            Transform3DGroup gameObjectMeshTransform = new Transform3DGroup();
            TranslateTransform3D gameObjectMeshTransformTranslate = new TranslateTransform3D();
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetY = 0;
            ScaleTransform3D gameObjectMeshTransformScale = new ScaleTransform3D();
            gameObjectMeshTransformScale.ScaleX = 0.1;
            gameObjectMeshTransformScale.ScaleY = 0.1;
            gameObjectMeshTransformScale.ScaleZ = 0.1;
            RotateTransform3D gameObjectMeshTransformRotation = new RotateTransform3D();
            gameObjectMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformTranslate);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformScale);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformRotation);
            gameObjectMeshGeometryModel.Transform = gameObjectMeshTransform;
            gameObjectMeshGeometryModel.Range = 50;
            gameObjectMeshGeometryModel.Position = new Point3D(0, 0, 0);
            Color lightColor = new Color();
            lightColor.R = 0;
            lightColor.G = 0;
            lightColor.B = 255;
            gameObjectMeshGeometryModel.Color = lightColor;
            transformXScale.Text = "1";
            transformYScale.Text = "1";
            transformZScale.Text = "1";
            transformXLocation.Text = "0";
            transformYLocation.Text = "0";
            transformZLocation.Text = "0";
            transformXRotation.Text = "0";
            transformYRotation.Text = "0";
            transformZRotation.Text = "0";
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
            newSceneCollectionItemIcon.Text = "Новый свет точка " + (space.Children.Count - 1).ToString();
            newSceneCollectionItemIcon.Margin = new Thickness(5, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemIcon);
            sceneCollection.Children.Insert(sceneCollection.Children.Count - 1, newSceneCollectionItem);
            newSceneCollectionItem.MouseUp += SelectMeshHandler;
            Dictionary<String, Object> newMesh = new Dictionary<String, Object>();
            newMesh.Add("index", ((int)(space.Children.IndexOf(gameObjectMesh))));
            newMesh.Add("name", newSceneCollectionItemIcon.Text);
            newMesh.Add("parent", "none");
            newMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            newMesh.Add("normals", 45);
            newMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            newMesh.Add("type", "pointLight");
            newMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
            meshs.Add(newMesh);
            transformSelectedMeshName.Text = newMesh["name"].ToString();
            ComboBoxItem selectableMesh = new ComboBoxItem();
            selectableMesh.Content = transformSelectedMeshName.Text;
            transformMeshSelector.Items.Insert(transformMeshSelector.Items.Count - 1, selectableMesh);
            transformMeshSelector.SelectedIndex = transformMeshSelector.Items.Count - 2;
            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;
            normalsAngle.Text = "45";
            textureXLocation.Text = "0";
            textureYLocation.Text = "1";
            textureZLocation.Text = "1";
            textureXScale.Text = "0";
            textureYScale.Text = "0";
            textureZScale.Text = "1";
        }

        private void CreateSpotLight()
        {
            ModelVisual3D gameObjectMesh = new ModelVisual3D();
            SpotLight gameObjectMeshGeometryModel = new SpotLight();
            Transform3DGroup gameObjectMeshTransform = new Transform3DGroup();
            TranslateTransform3D gameObjectMeshTransformTranslate = new TranslateTransform3D();
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetX = 0;
            gameObjectMeshTransformTranslate.OffsetY = 0;
            ScaleTransform3D gameObjectMeshTransformScale = new ScaleTransform3D();
            gameObjectMeshTransformScale.ScaleX = 0.1;
            gameObjectMeshTransformScale.ScaleY = 0.1;
            gameObjectMeshTransformScale.ScaleZ = 0.1;
            RotateTransform3D gameObjectMeshTransformRotation = new RotateTransform3D();
            gameObjectMeshTransformRotation.Rotation = new AxisAngleRotation3D(new Vector3D(0, 0, 0), 0);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformTranslate);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformScale);
            gameObjectMeshTransform.Children.Add(gameObjectMeshTransformRotation);
            gameObjectMeshGeometryModel.Transform = gameObjectMeshTransform;
            gameObjectMeshGeometryModel.Range = 50;
            gameObjectMeshGeometryModel.Position = new Point3D(0, 0, 0);
            Color lightColor = new Color();
            lightColor.R = 0;
            lightColor.G = 0;
            lightColor.B = 0;
            gameObjectMeshGeometryModel.Color = lightColor;
            transformXScale.Text = "1";
            transformYScale.Text = "1";
            transformZScale.Text = "1";
            transformXLocation.Text = "0";
            transformYLocation.Text = "0";
            transformZLocation.Text = "0";
            transformXRotation.Text = "0";
            transformYRotation.Text = "0";
            transformZRotation.Text = "0";
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
            newSceneCollectionItemIcon.Text = "Новый свет пятно " + (space.Children.Count - 1).ToString();
            newSceneCollectionItemIcon.Margin = new Thickness(5, 5, 5, 5);
            newSceneCollectionItem.Children.Add(newSceneCollectionItemIcon);
            sceneCollection.Children.Insert(sceneCollection.Children.Count - 1, newSceneCollectionItem);
            newSceneCollectionItem.MouseUp += SelectMeshHandler;
            Dictionary<String, Object> newMesh = new Dictionary<String, Object>();
            newMesh.Add("index", ((int)(space.Children.IndexOf(gameObjectMesh))));
            newMesh.Add("name", newSceneCollectionItemIcon.Text);
            newMesh.Add("parent", "none");
            newMesh.Add("modifiers", new List<Dictionary<String, Object>>());
            newMesh.Add("normals", 45);
            newMesh.Add("textures", new List<Int32>()
            {
                0, 1, 1, 1, 0, 0, 1, 0
            });
            newMesh.Add("type", "spotLight");
            newMesh.Add("textureSource", "https://cdn0.iconfinder.com/data/icons/summer-background/1200/Summer_Travel_Background-256.png");
            meshs.Add(newMesh);
            transformSelectedMeshName.Text = newMesh["name"].ToString();
            ComboBoxItem selectableMesh = new ComboBoxItem();
            selectableMesh.Content = transformSelectedMeshName.Text;
            transformMeshSelector.Items.Insert(transformMeshSelector.Items.Count - 1, selectableMesh);
            transformMeshSelector.SelectedIndex = transformMeshSelector.Items.Count - 2;
            modifiers.Children.Clear();
            embeddedModifiers.SelectedIndex = 0;
            normalsAngle.Text = "45";
            textureXLocation.Text = "0";
            textureYLocation.Text = "1";
            textureZLocation.Text = "1";
            textureXScale.Text = "0";
            textureYScale.Text = "0";
            textureZScale.Text = "1";
        }

        public static float Clamp(float val, float min, float max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        private void SetTexturePropsSourceHandler(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            bool? res = ofd.ShowDialog();
            if (res != false)
            {
                Stream myStream;
                if ((myStream = ofd.OpenFile()) != null)
                {
                    string file_name = ofd.FileName;
                    string file_text = File.ReadAllText(file_name);
                    int currentMeshIdx = 0;
                    foreach (Dictionary<String, Object> mesh in meshs)
                    {
                        if (((int)(mesh["index"])) == space.Children.IndexOf(selectedMesh))
                        {
                            currentMeshIdx = meshs.IndexOf(mesh);
                        }
                    }
                    meshs[currentMeshIdx]["textureSource"] = file_name;
                    MaterialGroup mg = ((MaterialGroup)(((GeometryModel3D)(selectedMesh.Content)).Material));
                    ImageBrush imageBrush = new ImageBrush();
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(file_name, UriKind.Absolute);
                    bitmapImage.EndInit();
                    imageBrush.ImageSource = bitmapImage;
                    ((DiffuseMaterial)(mg.Children[2])).Brush = imageBrush;
                }
            }
        }

        private void ToggleTexturePropsSettingsHandler(object sender, RoutedEventArgs e)
        {
            TextBlock toggler = ((TextBlock)(sender));
            if (texturePropsSettings.Visibility == Visibility.Visible)
            {
                texturePropsSettings.Visibility = Visibility.Collapsed;
                toggler.Text = "➤";
            }
            else if (texturePropsSettings.Visibility == Visibility.Collapsed)
            {
                texturePropsSettings.Visibility = Visibility.Visible;
                toggler.Text = "⮟";
            }
        }

    }
}
