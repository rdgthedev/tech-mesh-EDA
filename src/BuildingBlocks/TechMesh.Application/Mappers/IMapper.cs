namespace TechMesh.Application.Mappers;

public interface IMapper<in TSource, out TDestination>
{
    TDestination Map(TSource source);

    IEnumerable<TDestination> Map(IEnumerable<TSource> source);
}