using Domain.Entitites.Base;

namespace Application.Mapper.Base;

public interface IBaseMappper<TResponseDto, TEntity, TRequestDto> where TEntity : BaseEntity
{
    public TEntity ToEntity(TRequestDto input);
    public TResponseDto ToDto(TEntity input);
}