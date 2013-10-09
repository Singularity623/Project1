﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class Camera
    {
        #region Fields

        private GraphicsDeviceManager graphics;

        /// <summary>
        /// The eye position in space
        /// </summary>
        private Vector3 eye = new Vector3(1000, 1000, 1000);

        /// <summary>
        /// The location we are looking at in space.
        /// </summary>
        private Vector3 center = new Vector3(0, 0, 0);

        /// <summary>
        /// The up direction
        /// </summary>
        private Vector3 up = new Vector3(0, 1, 0);

        private float fov = MathHelper.ToRadians(35);
        private float znear = 1;
        private float zfar = 10000;

        private bool mousePitchYaw = true;
        private bool mousePanTilt = true;
        private bool padPitchYaw = true;

        private bool useChaseCamera = false;
        private Vector3 desiredEye = Vector3.Zero;
        private Vector3 desiredUp = Vector3.Zero;
        private Vector3 velocity = Vector3.Zero;

        private Vector3 rollVelocity = Vector3.Zero;

        private float stiffness = 100;
        private float damping = 60;

        private MouseState lastMouseState;

        private Matrix view;
        private Matrix projection;

        #endregion

        #region Properties

        public bool MousePitchYaw { get { return mousePitchYaw; } set { mousePitchYaw = value; } }
        public bool MousePanTilt { get { return mousePanTilt; } set { mousePanTilt = value; } }

        public Vector3 Center { get { return center; } set { center = value; ComputeView(); } }
        public Vector3 Up { get { return up; } set { up = value; ComputeView(); } }
        public Vector3 Eye { get { return eye; } set { eye = value; ComputeView(); } }

        public Matrix View { get { return view; } }
        public Matrix Projection { get { return projection; } }

        public bool UseChaseCamera { get { return useChaseCamera; } set { useChaseCamera = value; } }
        public Vector3 DesiredEye { get { return desiredEye; } set { desiredEye = value; } }
        public Vector3 DesiredUp { get { return desiredUp; } set { desiredUp = value; } }
        public float Stiffness { get { return stiffness; } set { stiffness = value; } }
        public float Damping { get { return damping; } set { damping = value; } }

        #endregion

        #region Construction and Initialization

        /// <summary>
        /// Constructor. Initializes the graphics field from a passed parameter.
        /// </summary>
        /// <param name="graphics">The graphics device manager for our program</param>
        public Camera(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        public void Initialize()
        {

            ComputeView();
            ComputeProjection();

            lastMouseState = Mouse.GetState();
        }

        #endregion

        #region Matrix Computations

        private void ComputeView()
        {
            view = Matrix.CreateLookAt(eye, center, up);
        }

        private void ComputeProjection()
        {
            projection = Matrix.CreatePerspectiveFieldOfView(fov,
                graphics.GraphicsDevice.Viewport.AspectRatio, znear, zfar);
        }

        public void Reset()
        {
            Initialize();
        }

        public void Pitch(float angle)
        {
            // Need a vector in the camera X direction
            Vector3 cameraZ = eye - center;
            Vector3 cameraX = Vector3.Cross(up, cameraZ);
            float len = cameraX.LengthSquared();
            if (len > 0)
                cameraX.Normalize();
            else
                cameraX = new Vector3(1, 0, 0);

            Matrix t1 = Matrix.CreateTranslation(-center);
            Matrix r = Matrix.CreateFromAxisAngle(cameraX, angle);
            Matrix t2 = Matrix.CreateTranslation(center);

            Matrix M = t1 * r * t2;
            eye = Vector3.Transform(eye, M);
            ComputeView();
        }

        public void Yaw(float angle)
        {
            // Need a vector in the camera X direction
            Vector3 cameraZ = eye - center;
            Vector3 cameraX = Vector3.Cross(up, cameraZ);
            Vector3 cameraY = Vector3.Cross(cameraZ, cameraX);
            float len = cameraY.LengthSquared();
            if (len > 0)
                cameraY.Normalize();
            else
                cameraY = new Vector3(0, 1, 0);

            Matrix t1 = Matrix.CreateTranslation(-center);
            Matrix r = Matrix.CreateFromAxisAngle(cameraY, angle);
            Matrix t2 = Matrix.CreateTranslation(center);

            Matrix M = t1 * r * t2;
            eye = Vector3.Transform(eye, M);
            ComputeView();
        }

        public void Pan(float angle)
        {
            Vector3 cameraZ = eye - center;
            Vector3 cameraX = Vector3.Cross(up, cameraZ);
            Vector3 cameraY = Vector3.Cross(cameraZ, cameraX);
            float len = cameraY.LengthSquared();
            if (len > 0)
                cameraY.Normalize();
            else
                cameraY = new Vector3(0, 1, 0);

            Matrix t1 = Matrix.CreateTranslation(-eye);
            Matrix r = Matrix.CreateFromAxisAngle(cameraY, angle);
            Matrix t2 = Matrix.CreateTranslation(eye);

            Matrix M = t1 * r * t2;
            center = Vector3.Transform(center, M);
            ComputeView();
        }

        public void Tilt(float angle)
        {
            Vector3 cameraZ = eye - center;
            Vector3 cameraX = Vector3.Cross(up, cameraZ);
            float len = cameraX.LengthSquared();
            if (len > 0)
                cameraX.Normalize();
            else
                cameraX = new Vector3(1, 0, 0);

            Matrix t1 = Matrix.CreateTranslation(-eye);
            Matrix r = Matrix.CreateFromAxisAngle(cameraX, angle);
            Matrix t2 = Matrix.CreateTranslation(eye);

            Matrix M = t1 * r * t2;
            center = Vector3.Transform(center, M);
            ComputeView();
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mousePitchYaw && mouseState.LeftButton == ButtonState.Pressed &&
                lastMouseState.LeftButton == ButtonState.Pressed)
            {
                float changeY = mouseState.Y - lastMouseState.Y;
                Pitch(-changeY * 0.005f);

                float changeX = mouseState.X - lastMouseState.X;
                Yaw(changeX * 0.005f);
            }
            if (mousePanTilt && mouseState.RightButton == ButtonState.Pressed &&
                lastMouseState.RightButton == ButtonState.Pressed)
            {
                float changeY = mouseState.Y - lastMouseState.Y;
                Tilt(changeY * 0.005f);

                float changeX = mouseState.X - lastMouseState.X;
                Pan(-changeX * 0.005f);
            }

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (padPitchYaw)
            {
                Yaw(-gamePadState.ThumbSticks.Right.X * 0.05f);
                Pitch(gamePadState.ThumbSticks.Right.Y * 0.05f);
            }

            if (useChaseCamera)
            {
                // Calculate spring force
                Vector3 stretch = desiredEye - eye;
                Vector3 acceleration = stretch * stiffness - velocity * damping;

                //Calculate roll Spring force
                Vector3 rollStretch = desiredUp - up;
                Vector3 rollAcceleration = rollStretch * stiffness - rollVelocity * damping;

                // Apply acceleration
                velocity += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
                rollVelocity += rollAcceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Apply velocity
                eye += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                up += rollVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            lastMouseState = mouseState;
        }

        #endregion
    }

}
