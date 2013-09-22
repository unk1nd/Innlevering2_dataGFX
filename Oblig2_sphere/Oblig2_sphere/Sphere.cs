/**************************************************************
 * Datamaskingrafikk - Obligatorisk innlevering 2
 * Mikael Bendiksen
 * 20.September 2013
 **************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Oblig2_sphere
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Sphere : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        private GraphicsDevice device;
        private SpriteBatch spriteBatch;

        private BasicEffect effect;

        private VertexPositionColor[] VertexList;
        private VertexPositionColor[] VertexListNorth;
        private VertexPositionColor[] VertexListSouth;

        //Camera
        private Vector3 cameraPosition = new Vector3(3.5f, 2.0f, 4.0f);
        private Vector3 cameraTarget = Vector3.Zero;
        private Vector3 cameraUpVector = new Vector3(0.0f, 1.0f, 0.0f);

        //WVP
        private Matrix world;
        private Matrix projection;
        private Matrix view;

        public Sphere()
        {
            graphics = new GraphicsDeviceManager(this);

            content = new ContentManager(this.Services);

            //Gjør at musepekeren er synlig over vinduet:
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            initDevice();
            initCamera();
            initSphere();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// Initializes the device
        /// </summary>
        private void initDevice()
        {
            device = graphics.GraphicsDevice;

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 900;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Oblig2 - Sphere - Mikael Bendiksen";
            effect = new BasicEffect(graphics.GraphicsDevice);
        }

        /// <summary>
        /// Initializes the camera
        /// </summary>
        private void initCamera()
        {
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out view);
            Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.01f, 1000.0f, out projection);

            effect.Projection = projection;
            effect.View = view;
        }

        /// <summary>
        /// Initializes the sphere.
        /// </summary>
        private void initSphere()
        {
            VertexListNorth = new VertexPositionColor[38];
            VertexListSouth = new VertexPositionColor[38];
            VertexList = new VertexPositionColor[342];

            float c = (float)Math.PI / 180.0f;
            float phir = 0.0f;
            float phir20 = 0.0f;
            float thetar = 0.0f;
            float x = 0.0f, y = 0.0f, z = 0.0f;
            int i = 0;

            // Oppgave 1
            for (float phi = -80.0f; phi <= 80.0f; phi += 20.0f)
            {
                phir = c * phi;
                phir20 = c * (phi + 20);

                for (float theta = -180.0f; theta <= 180.0f; theta += 20.0f)
                {
                    thetar = c * theta;

                    // X, Y, Z for (2n - 1) hvor n > 0
                    x = (float)Math.Sin(thetar) * (float)Math.Cos(phir);
                    y = (float)Math.Cos(thetar) * (float)Math.Cos(phir);
                    z = (float)Math.Sin(phir);

                    VertexList[i].Position = new Vector3(x, y, z);
                    VertexList[i].Color = Color.Blue;
                    i++;

                    // X, Y, Z for (2n) hvor n > 0
                    x = (float)Math.Sin(thetar) * (float)Math.Cos(phir20);
                    y = (float)Math.Cos(thetar) * (float)Math.Cos(phir20);
                    z = (float)Math.Sin(phir20);

                    VertexList[i].Position = new Vector3(x, y, z);
                    VertexList[i].Color = Color.Blue;
                    i++;
                }
            }

            // Oppgave 2
            i = 0;
            phir = 90.0f * c;
            for (float theta = -180.0f; theta <= 180.0f; theta += 20.0f)
            {
                thetar = c * theta;

                // X, Y, Z for (2n - 1) hvor n > 0
                x = (float)Math.Sin(thetar) * (float)Math.Cos(phir);
                y = (float)Math.Cos(thetar) * (float)Math.Cos(phir);
                z = (float)Math.Sin(phir);

                VertexListNorth[i].Position = new Vector3(x, y, z);
                VertexListNorth[i].Color = Color.Red;
                VertexListSouth[i].Position = new Vector3(x, y, -z);
                VertexListSouth[i].Color = Color.Red;
                i++;

                // X, Y, Z for (2n) hvor n > 0
                x = (float)Math.Sin(thetar) * (float)Math.Cos(phir20);
                y = (float)Math.Cos(thetar) * (float)Math.Cos(phir20);
                z = (float)Math.Sin(phir20);

                VertexListNorth[i].Position = new Vector3(x, y, z);
                VertexListNorth[i].Color = Color.Red;
                VertexListSouth[i].Position = new Vector3(x, y, -z);
                VertexListSouth[i].Color = Color.Red;
                i++;
            }
            effect.VertexColorEnabled = true;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            RasterizerState rasterizerState1 = new RasterizerState();
            rasterizerState1.CullMode = CullMode.None;
            rasterizerState1.FillMode = FillMode.WireFrame;
            device.RasterizerState = rasterizerState1;

            device.Clear(Color.Black);

            //Setter world=I:
            world = Matrix.Identity;
            
            // Setter world-matrisa på effect-objektet (verteks-shaderen):
            effect.World = world;

            //Starter tegning - må bruke effect-objektet:
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                // Angir primitivtype, aktuelle vertekser, en offsetverdi og antall
                // primitiver (her 1 siden verteksene beskriver en trekant):

                if (VertexList != null)
                {
                    device.DrawUserPrimitives(PrimitiveType.TriangleStrip, VertexList, 0, 340, VertexPositionColor.VertexDeclaration);
                    device.DrawUserPrimitives(PrimitiveType.TriangleStrip, VertexListNorth, 0, 36, VertexPositionColor.VertexDeclaration);
                    device.DrawUserPrimitives(PrimitiveType.TriangleStrip, VertexListSouth, 0, 36, VertexPositionColor.VertexDeclaration);
                }
            }

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}

/***************
* End of Code!!
****************/