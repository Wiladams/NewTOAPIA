using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA
{
    public delegate void ToVoid<T>(T x);
    public delegate void ToVoid<T, U>(T x, U y);
    public delegate void ToVoid<T, U, V>(T x, U y, V z);
    public delegate void ToVoid<T, U, V, W>(T x, U y, V z, W w);

    public delegate R ToLastType<R>();
    public delegate R ToLastType<T, R>(T x);
    public delegate R ToLastType<T, U, R>(T x, U y);
    public delegate R ToLastType<T, U, V, R>(T x, U y, V z);
    public delegate R ToLastType<T, U, V, W, R>(T x, U y, V z, W w);
}
