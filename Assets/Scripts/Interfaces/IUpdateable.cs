public interface IUpdateable 
{
	bool Enabled { get; }
	void OnUpdate(float deltaTime);
}
