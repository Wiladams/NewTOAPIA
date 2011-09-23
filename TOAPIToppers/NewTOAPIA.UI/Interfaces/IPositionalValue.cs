namespace NewTOAPIA.UI
{
	public interface IPositionalValue
	{	
		void	SetValue(float newValue, bool invoke);
		void	SetValueBy(int x, int y, bool invoke);

		void	SetPosition(float newValue, bool invoke);
		void	SetPositionBy(int x, int y, bool invoke);

		void	SetRange(float min, float max);
		void	GetRange(ref float min, ref float max);

		float Value
		{get;}
		
		float Position
		{get;}
		
	
		void	ValueChanged(float newValue, bool invoke);
		void	PositionChanged(float newValue, bool invoke);
	}
}