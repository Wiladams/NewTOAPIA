using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    //[StructLayout(LayoutKind.Sequential)]
    public class Matrix4
    {
        #region Fields

        ///// <summary>
        ///// Top row of the matrix
        ///// </summary>
        //public float4 Row0;
        ///// <summary>
        ///// 2nd row of the matrix
        ///// </summary>
        //public float4 Row1;
        ///// <summary>
        ///// 3rd row of the matrix
        ///// </summary>
        //public float4 Row2;
        ///// <summary>
        ///// Bottom row of the matrix
        ///// </summary>
        //public float4 Row3;

        // Column major storage to match what OpenGL expects
        // |  0  4  8  12  |   |  RS R  R  T  |
        // |  1  5  9  13  |   |  R  RS R  T  |
        // |  2  6  10 14  | = |  R  R  RS T  |
        // |  3  7  11 15  |   |  0  0  0  1  |
        float[] _m = new float[16];

        /// <summary>
        /// The identity matrix
        /// </summary>
        static public readonly Matrix4 Identity = new Matrix4(new float[] { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 });


        #endregion

        #region Constructors

        public Matrix4()
        {
            SetToIdentity();
        }

        public Matrix4(float[] elements)
        {
        }

        /// <summary>
        /// Construct a new matrix from 4 vectors representing each row
        /// </summary>
        /// <param name="row0">Top row of the matrix</param>
        /// <param name="row1">2nd row of the matrix</param>
        /// <param name="row2">3rd row of the matrix</param>
        /// <param name="row3">Bottom row of the matrix</param>
        public Matrix4(float4 row0, float4 row1, float4 row2, float4 row3)
        {
            Row0 = row0;
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
        }

        //public Matrix4()
        //{
        //    Row0 = new float4(1, 0, 0, 0);
        //    Row1 = new float4(0, 1, 0, 0);
        //    Row2 = new float4(0, 0, 1, 0);
        //    Row3 = new float4(0, 0, 0, 1);
        //}

        #endregion

        #region Functions
        #region SetToIdentity
        public void SetToIdentity()
        {
            _m[0] = 1;
            _m[1] = 0;
            _m[2] = 0;
            _m[3] = 0;
            _m[4] = 0;
            _m[5] = 1;
            _m[6] = 0;
            _m[7] = 0;
            _m[8] = 0;
            _m[9] = 0;
            _m[10] = 1;
            _m[11] = 0;
            _m[12] = 0;
            _m[13] = 0;
            _m[14] = 0;
            _m[15] = 1;
        }
        #endregion

        //public Matrix4 GetInverse()
        //{
        //    Matrix4 m = new Matrix4(this);
        //    m.Invert();
        //    return m;
        //}

        #region public void Invert()

        //public void Invert()
        //{
        //    double[] matrix = Utils.create_m4();
        //    double[] matrixInverse = Utils.create_m4();

        //    Utils.transform_2_m4(this, matrix);

        //    if (Utils.m4_inverse(matrixInverse, matrix) == 0)
        //    {
        //        //Debug.WriteLine("Matrix:");
        //        //Debug.WriteLine(this.ToString());
        //        //Debug.WriteLine("Determinant: " + this.GetDeterminant());
        //        throw new MatrixNotInvertableException("Matrix Not Invertable");
        //    }

        //    Utils.m4_2_transform(matrixInverse, this);
        //}

        #endregion

        #region public void Transpose()
        public void Transpose()
        {
            Set(new float[]{
                _m[0], _m[4], _m[8], _m[12],
                _m[1], _m[5], _m[9], _m[13],
                _m[2], _m[6], _m[10], _m[14],
                _m[3], _m[7], _m[11], _m[15]});
        }
#endregion

        #region Translate
        public void Translate(float x, float y, float z)
        {
            _m[12] += x;
            _m[13] += y;
            _m[14] += z; 
        }
        #endregion

        #region Scale
        public void Scale(float x, float y, float z)
        {
            _m[0] *= x; _m[5] *= y; _m[10] *= z;
        }
        #endregion

        #endregion

        #region Properties
        public void Set(float[] vec)
        {
            _m[0] = vec[0];
            _m[1] = vec[1];
            _m[2] = vec[2];
            _m[3] = vec[3];
            _m[4] = vec[4];
            _m[5] = vec[5];
            _m[6] = vec[6];
            _m[7] = vec[7];
            _m[8] = vec[8];
            _m[9] = vec[9];
            _m[10] = vec[10];
            _m[11] = vec[11];
            _m[12] = vec[12];
            _m[13] = vec[13];
            _m[14] = vec[14];
            _m[15] = vec[15];
        }

        /// <summary>
        /// The determinant of this matrix
        /// </summary>
        public float Determinant
        {
            get
            {
                return
                    Row0.x * Row1.y * Row2.z * Row3.w - Row0.x * Row1.y * Row2.w * Row3.z + Row0.x * Row1.z * Row2.w * Row3.y - Row0.x * Row1.z * Row2.y * Row3.w
                  + Row0.x * Row1.w * Row2.y * Row3.z - Row0.x * Row1.w * Row2.z * Row3.y - Row0.y * Row1.z * Row2.w * Row3.x + Row0.y * Row1.z * Row2.x * Row3.w
                  - Row0.y * Row1.w * Row2.x * Row3.z + Row0.y * Row1.w * Row2.z * Row3.x - Row0.y * Row1.x * Row2.z * Row3.w + Row0.y * Row1.x * Row2.w * Row3.z
                  + Row0.z * Row1.w * Row2.x * Row3.y - Row0.z * Row1.w * Row2.y * Row3.x + Row0.z * Row1.x * Row2.y * Row3.w - Row0.z * Row1.x * Row2.w * Row3.y
                  + Row0.z * Row1.y * Row2.w * Row3.x - Row0.z * Row1.y * Row2.x * Row3.w - Row0.w * Row1.x * Row2.y * Row3.z + Row0.w * Row1.x * Row2.z * Row3.y
                  - Row0.w * Row1.y * Row2.z * Row3.x + Row0.w * Row1.y * Row2.x * Row3.z - Row0.w * Row1.z * Row2.x * Row3.y + Row0.w * Row1.z * Row2.y * Row3.x;
            }
        }

        #region Columns
        /// <summary>
        /// The first column of this matrix
        /// </summary>
        public float4 Column0
        {
            get { return new float4(_m[0], _m[1], _m[2], _m[3]); }
        }

        /// <summary>
        /// The second column of this matrix
        /// </summary>
        public float4 Column1
        {
            get { return new float4(_m[4], _m[5], _m[6], _m[7]); }
        }

        /// <summary>
        /// The third column of this matrix
        /// </summary>
        public float4 Column2
        {
            get { return new float4(_m[8], _m[9], _m[10], _m[11]); }
        }

        /// <summary>
        /// The fourth column of this matrix
        /// </summary>
        public float4 Column3
        {
            get { return new float4(_m[12], _m[13], _m[14], _m[15]); }
        }
        #endregion

        // Rows of the Matrix as Vectors
        // |  0  4  8  12  |   |  RS R  R  T  |
        // |  1  5  9  13  |   |  R  RS R  T  |
        // |  2  6  10 14  | = |  R  R  RS T  |
        // |  3  7  11 15  |   |  0  0  0  1  |

        #region Rows
        public float4 Row0
        {
            get
            {
                float4 vec = new float4(_m[0], _m[4], _m[8], _m[12]);
                return vec;
            }

            set
            {
                _m[0] = value.x; _m[4] = value.y; _m[8] = value.z; _m[12] = value.w;
            }
        }

        public float4 Row1
        {
            get
            {
                float4 vec = new float4(_m[1], _m[5], _m[9], _m[13]);
                return vec;
            }

            set
            {
                _m[1] = value.x; _m[5] = value.y; _m[9] = value.z; _m[13] = value.w;
            }
        }
        public float4 Row2
        {
            get
            {
                float4 vec = new float4(_m[2], _m[6], _m[10], _m[14]);
                return vec;
            }

            set
            {
                _m[2] = value.x; _m[6] = value.y; _m[10] = value.z; _m[14] = value.w;
            }
        }
        public float4 Row3
        {
            get
            {
                float4 vec = new float4(_m[3], _m[7], _m[11], _m[15]);
                return vec;
            }

            set
            {
                _m[3] = value.x; _m[7] = value.y; _m[11] = value.z; _m[15] = value.w;
            }
        }
        #endregion
        #endregion

        #region Operator overloads

        /// <summary>
        /// Matrix multiplication
        /// </summary>
        /// <param name="left">left-hand operand</param>
        /// <param name="right">right-hand operand</param>
        /// <returns>A new Matrix44 which holds the result of the multiplication</returns>
        public static Matrix4 operator *(Matrix4 left, Matrix4 right)
        {
            return Matrix4.Mult(left, right);
        }

        public float get(int x, int y)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Static functions

        #region Scale Functions

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="scale">Single scale factor for x,y and z axes</param>
        /// <returns>A scaling matrix</returns>
        //public static Matrix4 Scale(float scale)
        //{
        //    return Scale(scale, scale, scale);
        //}

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="scale">Scale factors for x,y and z axes</param>
        /// <returns>A scaling matrix</returns>
        //public static Matrix4 Scale(Vector3f scale)
        //{
        //    return Scale(scale.x, scale.y, scale.z);
        //}

        /// <summary>
        /// Build a scaling matrix
        /// </summary>
        /// <param name="x">Scale factor for x-axis</param>
        /// <param name="y">Scale factor for y-axis</param>
        /// <param name="z">Scale factor for z-axis</param>
        /// <returns>A scaling matrix</returns>
        //public static Matrix4 Scale(float x, float y, float z)
        //{
        //    Matrix4 result = new Matrix4();
        //    result.Row0 = float4.UnitX * x;
        //    result.Row1 = float4.UnitY * y;
        //    result.Row2 = float4.UnitZ * z;
        //    result.Row3 = float4.UnitW;
        //    return result;
        //}

        #endregion

        #region Translation Functions

        /// <summary>
        /// Build a translation matrix with the given translation
        /// </summary>
        /// <param name="trans">The vector to translate along</param>
        /// <returns>A Translation matrix</returns>
        public static Matrix4 Translation(Vector3f trans)
        {
            return Translation(trans.x, trans.y, trans.z);
        }

        /// <summary>
        /// Build a translation matrix with the given translation
        /// </summary>
        /// <param name="x">X translation</param>
        /// <param name="y">Y translation</param>
        /// <param name="z">Z translation</param>
        /// <returns>A Translation matrix</returns>
        public static Matrix4 Translation(float x, float y, float z)
        {
            Matrix4 result = Identity;
            result.Row3 = new float4(x, y, z, 1.0f);
            return result;
        }


        #endregion

        #region Rotation Functions

        /// <summary>
        /// Build a rotation matrix that rotates about the x-axis
        /// </summary>
        /// <param name="angle">angle in radians to rotate counter-clockwise around the x-axis</param>
        /// <returns>A rotation matrix</returns>
        public static Matrix4 RotateX(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            Matrix4 result = new Matrix4() ;
            result.Row0 = float4.UnitX;
            result.Row1 = new float4(0.0f, cos, sin, 0.0f);
            result.Row2 = new float4(0.0f, -sin, cos, 0.0f);
            result.Row3 = float4.UnitW;
            return result;
        }

        /// <summary>
        /// Build a rotation matrix that rotates about the y-axis
        /// </summary>
        /// <param name="angle">angle in radians to rotate counter-clockwise around the y-axis</param>
        /// <returns>A rotation matrix</returns>
        public static Matrix4 RotateY(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            Matrix4 result = new Matrix4();
            result.Row0 = new float4(cos, 0.0f, -sin, 0.0f);
            result.Row1 = float4.UnitY;
            result.Row2 = new float4(sin, 0.0f, cos, 0.0f);
            result.Row3 = float4.UnitW;
            return result;
        }

        /// <summary>
        /// Build a rotation matrix that rotates about the z-axis
        /// </summary>
        /// <param name="angle">angle in radians to rotate counter-clockwise around the z-axis</param>
        /// <returns>A rotation matrix</returns>
        public static Matrix4 RotateZ(float angle)
        {
            float cos = (float)System.Math.Cos(angle);
            float sin = (float)System.Math.Sin(angle);

            Matrix4 result = new Matrix4() ;
            result.Row0 = new float4(cos, sin, 0.0f, 0.0f);
            result.Row1 = new float4(-sin, cos, 0.0f, 0.0f);
            result.Row2 = float4.UnitZ;
            result.Row3 = float4.UnitW;
            return result;
        }

        /// <summary>
        /// Build a rotation matrix to rotate about the given axis
        /// </summary>
        /// <param name="axis">the axis to rotate about</param>
        /// <param name="angle">angle in radians to rotate counter-clockwise (looking in the direction of the given axis)</param>
        /// <returns>A rotation matrix</returns>
        public static Matrix4 Rotate(float3 axis, float angle)
        {
            float cos = (float)System.Math.Cos(-angle);
            float sin = (float)System.Math.Sin(-angle);
            float t = 1.0f - cos;

            axis = axis.Normalize;

            Matrix4 result = new Matrix4();
            result.Row0 = new float4(t * axis.x * axis.x + cos, t * axis.x * axis.y - sin * axis.z, t * axis.x * axis.z + sin * axis.y, 0.0f);
            result.Row1 = new float4(t * axis.x * axis.y + sin * axis.z, t * axis.y * axis.y + cos, t * axis.y * axis.z - sin * axis.x, 0.0f);
            result.Row2 = new float4(t * axis.x * axis.z - sin * axis.y, t * axis.y * axis.z + sin * axis.x, t * axis.z * axis.z + cos, 0.0f);
            result.Row3 = float4.UnitW;
            return result;
        }

        /// <summary>
        /// Build a rotation matrix from a quaternion
        /// </summary>
        /// <param name="q">the quaternion</param>
        /// <returns>A rotation matrix</returns>
        public static Matrix4 Rotate(Quaternion q)
        {
            float3 axis;
            float angle;
            q.ToAxisAngle(out axis, out angle);
            return Rotate(axis, angle);
        }

        #endregion

        #region Camera Helper Functions

        /// <summary>
        /// Build a world space to camera space matrix
        /// </summary>
        /// <param name="eye">Eye (camera) position in world space</param>
        /// <param name="target">Target position in world space</param>
        /// <param name="up">Up vector in world space (should not be parallel to the camera direction, that is target - eye)</param>
        /// <returns>A Matrix that transforms world space to camera space</returns>
        public static Matrix4 LookAt(Vector3f eye, Vector3f target, Vector3f up)
        {
            Vector3f z = Vector3f.Normalize(eye - target);
            Vector3f x = Vector3f.Normalize(Vector3f.Cross(up, z));
            Vector3f y = Vector3f.Normalize(Vector3f.Cross(z, x));

            Matrix4 rot = new Matrix4(new float4(x.x, y.x, z.x, 0.0f),
                                        new float4(x.y, y.y, z.y, 0.0f),
                                        new float4(x.z, y.z, z.z, 0.0f),
                                        float4.UnitW);

            Matrix4 trans = Matrix4.Translation(-eye);

            return trans * rot;
        }

        /// <summary>
        /// Build a projection matrix
        /// </summary>
        /// <param name="left">Left edge of the view frustum</param>
        /// <param name="right">Right edge of the view frustum</param>
        /// <param name="bottom">Bottom edge of the view frustum</param>
        /// <param name="top">Top edge of the view frustum</param>
        /// <param name="near">Distance to the near clip plane</param>
        /// <param name="far">Distance to the far clip plane</param>
        /// <returns>A projection matrix that transforms camera space to raster space</returns>
        public static Matrix4 Frustum(float left, float right, float bottom, float top, float near, float far)
        {
            float invRL = 1.0f / (right - left);
            float invTB = 1.0f / (top - bottom);
            float invFN = 1.0f / (far - near);
            return new Matrix4(new float4(2.0f * near * invRL, 0.0f, (right + left) * invRL, 0.0f),
                                new float4(0.0f, 2.0f * near * invTB, (top + bottom) * invTB, 0.0f),
                                new float4(0.0f, 0.0f, -(far + near) * invFN, -1.0f),
                                new float4(0.0f, 0.0f, -2.0f * far * near * invFN, 0.0f));
        }

        /// <summary>
        /// Build a projection matrix
        /// </summary>
        /// <param name="fovy">Angle of the field of view in the y direction (in radians)</param>
        /// <param name="aspect">Aspect ratio of the view (width / height)</param>
        /// <param name="near">Distance to the near clip plane</param>
        /// <param name="far">Distance to the far clip plane</param>
        /// <returns>A projection matrix that transforms camera space to raster space</returns>
        public static Matrix4 Perspective(float fovy, float aspect, float near, float far)
        {
            float yMax = near * (float)System.Math.Tan(0.5f * fovy);
            float yMin = -yMax;
            float xMin = yMin * aspect;
            float xMax = yMax * aspect;

            return Frustum(xMin, xMax, yMin, yMax, near, far);
        }

        #endregion

        #region Multiply Functions

        /// <summary>
        /// Post multiply this matrix by another matrix
        /// </summary>
        /// <param name="right">The matrix to multiply</param>
        /// <returns>A new Matrix44 that is the result of the multiplication</returns>
        public static Matrix4 Mult(Matrix4 left, Matrix4 right)
        {
            float4 col0 = right.Column0;
            float4 col1 = right.Column1;
            float4 col2 = right.Column2;
            float4 col3 = right.Column3;

            left.Row0 = new float4(left.Row0*col0, left.Row0*col1, left.Row0*col2, left.Row0*col3);
            left.Row1 = new float4(left.Row1*col0, left.Row1*col1, left.Row1*col2, left.Row1*col3);
            left.Row2 = new float4(left.Row2*col0, left.Row2*col1, left.Row2*col2, left.Row2*col3);
            left.Row3 = new float4(left.Row3*col0, left.Row3*col1, left.Row3*col2, left.Row3*col3);
            
            return left;
        }

        public static void Mult(ref Matrix4 left, ref Matrix4 right, ref Matrix4 result)
        {
            float4 col0 = right.Column0;
            float4 col1 = right.Column1;
            float4 col2 = right.Column2;
            float4 col3 = right.Column3;

            result.Row0 = new float4(left.Row0 * col0, left.Row0 * col1, left.Row0 * col2, left.Row0 * col3);
            result.Row1 = new float4(left.Row1 * col0, left.Row1 * col1, left.Row1 * col2, left.Row1 * col3);
            result.Row2 = new float4(left.Row2 * col0, left.Row2 * col1, left.Row2 * col2, left.Row2 * col3);
            result.Row3 = new float4(left.Row3 * col0, left.Row3 * col1, left.Row3 * col2, left.Row3 * col3);
        }

        #endregion

        #region Invert Functions

        /// <summary>
        /// Calculate the inverse of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to invert</param>
        /// <returns>The inverse of the given matrix if it has one, or the input if it is singular</returns>
        /// <exception cref="InvalidOperationException">Thrown if the Matrix4 is singular.</exception>
        public static Matrix4 Invert(Matrix4 mat)
        {
            int[] colIdx = { 0, 0, 0, 0 };
            int[] rowIdx = { 0, 0, 0, 0 };
            int[] pivotIdx = { -1, -1, -1, -1 };

            // convert the matrix to an array for easy looping
            float[,] inverse = {{mat.Row0.x, mat.Row0.y, mat.Row0.z, mat.Row0.w}, 
								{mat.Row1.x, mat.Row1.y, mat.Row1.z, mat.Row1.w}, 
								{mat.Row2.x, mat.Row2.y, mat.Row2.z, mat.Row2.w}, 
								{mat.Row3.x, mat.Row3.y, mat.Row3.z, mat.Row3.w} };
            int icol = 0;
            int irow = 0;
            for (int i = 0; i < 4; i++)
            {
                // Find the largest pivot value
                float maxPivot = 0.0f;
                for (int j = 0; j < 4; j++)
                {
                    if (pivotIdx[j] != 0)
                    {
                        for (int k = 0; k < 4; ++k)
                        {
                            if (pivotIdx[k] == -1)
                            {
                                float absVal = System.Math.Abs(inverse[j, k]);
                                if (absVal > maxPivot)
                                {
                                    maxPivot = absVal;
                                    irow = j;
                                    icol = k;
                                }
                            }
                            else if (pivotIdx[k] > 0)
                            {
                                return mat;
                            }
                        }
                    }
                }

                ++(pivotIdx[icol]);

                // Swap rows over so pivot is on diagonal
                if (irow != icol)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        float f = inverse[irow, k];
                        inverse[irow, k] = inverse[icol, k];
                        inverse[icol, k] = f;
                    }
                }

                rowIdx[i] = irow;
                colIdx[i] = icol;

                float pivot = inverse[icol, icol];
                // check for singular matrix
                if (pivot == 0.0f)
                {
                    throw new InvalidOperationException("Matrix is singular and cannot be inverted.");
                    //return mat;
                }

                // Scale row so it has a unit diagonal
                float oneOverPivot = 1.0f / pivot;
                inverse[icol, icol] = 1.0f;
                for (int k = 0; k < 4; ++k)
                    inverse[icol, k] *= oneOverPivot;

                // Do elimination of non-diagonal elements
                for (int j = 0; j < 4; ++j)
                {
                    // check this isn't on the diagonal
                    if (icol != j)
                    {
                        float f = inverse[j, icol];
                        inverse[j, icol] = 0.0f;
                        for (int k = 0; k < 4; ++k)
                            inverse[j, k] -= inverse[icol, k] * f;
                    }
                }
            }

            for (int j = 3; j >= 0; --j)
            {
                int ir = rowIdx[j];
                int ic = colIdx[j];
                for (int k = 0; k < 4; ++k)
                {
                    float f = inverse[k, ir];
                    inverse[k, ir] = inverse[k, ic];
                    inverse[k, ic] = f;
                }
            }

            mat.Row0 = new float4(inverse[0, 0], inverse[0, 1], inverse[0, 2], inverse[0, 3]);
            mat.Row1 = new float4(inverse[1, 0], inverse[1, 1], inverse[1, 2], inverse[1, 3]);
            mat.Row2 = new float4(inverse[2, 0], inverse[2, 1], inverse[2, 2], inverse[2, 3]);
            mat.Row3 = new float4(inverse[3, 0], inverse[3, 1], inverse[3, 2], inverse[3, 3]);
            return mat;
        }

        #endregion

        #region Transpose

        /// <summary>
        /// Calculate the transpose of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to transpose</param>
        /// <returns>The transpose of the given matrix</returns>
        public static Matrix4 Transpose(Matrix4 mat)
        {
            return new Matrix4(mat.Column0, mat.Column1, mat.Column2, mat.Column3);
        }


        /// <summary>
        /// Calculate the transpose of the given matrix
        /// </summary>
        /// <param name="mat">The matrix to transpose</param>
        public static void Transpose(ref Matrix4 mat, ref Matrix4 result)
        {
            result.Row0 = mat.Column0;
            result.Row1 = mat.Column1;
            result.Row2 = mat.Column2;
            result.Row3 = mat.Column3;
        }

        #endregion

        #endregion

        #region public override string ToString()

        /// <summary>
        /// Returns a System.String that represents the current Matrix44.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}\n{1}\n{2}\n{3}", Row0, Row1, Row2, Row3);
        }

        #endregion
    }

    public class MatrixNotInvertableException : ApplicationException
    {
        /// <summary>
        /// An attempt to invert a non-invertable matrix was made.
        /// </summary>
        /// <param name="msg"></param>
        public MatrixNotInvertableException(string msg) : base(msg) { }
    }

}
