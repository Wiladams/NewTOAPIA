
namespace NewTOAPIA.Drawing
{
    using System;
    using System.ServiceModel;

    using NewTOAPIA.Graphics;

    [ServiceContract]
    public interface IGraphState
    {
        // State Management
        [OperationContract(IsOneWay = true)]
        void Flush();
        
        [OperationContract(IsOneWay = true)]
        void SaveState();
        
        [OperationContract(IsOneWay = true)]
        void ResetState();
        
        [OperationContract(IsOneWay = true)]
        void RestoreState(int relative);

        // Setting Attributes and modes
        [OperationContract(IsOneWay = true)]
        void SetTextColor(uint colorref);
        
        [OperationContract(IsOneWay = true)]
        void SetBkColor(uint colorref);

        // Setting pens and brushes objects
        [OperationContract(IsOneWay = true)]
        void SetPen(IPen aPen);
        
        [OperationContract(IsOneWay = true)]
        void SetBrush(IBrush aBrush);
        
        [OperationContract(IsOneWay = true)]
        void SetFont(IFont aFont);

        //void SelectStockObject(int objectIndex);
        [OperationContract(IsOneWay = true)]
        void SelectUniqueObject(Guid objectID);

        // Setting some modes
        [OperationContract(IsOneWay = true)]
        void SetBkMode(int aMode);
        
        [OperationContract(IsOneWay = true)]
        void SetMappingMode(MappingModes aMode);
        
        [OperationContract(IsOneWay = true)]
        void SetPolyFillMode(PolygonFillMode aMode);
        
        [OperationContract(IsOneWay = true)]
        void SetROP2(BinaryRasterOps rasOp);

        // Viewport management
        //void SetViewportExtent(int width, int height);
        //void SetViewportOrigin(int x, int y);

        //void SetWindowExtent(int width, int height);
        //void SetWindowOrigin(int x, int y);
        //void OffsetWindowOrigin(int cx, int cy);
        [OperationContract(IsOneWay = true)]
        void SetClipRectangle(RectangleI rect);

        [OperationContract(IsOneWay = true)]
        void SetWorldTransform(Transformation wTransform);
        
        [OperationContract(IsOneWay = true)]
        void TranslateTransform(int dx, int dy);
        
        [OperationContract(IsOneWay = true)]
        void ScaleTransform(float xFactor, float yFactor);
        
        [OperationContract(IsOneWay = true)]
        void RotateTransform(float angle, int x, int y);
    }
}
