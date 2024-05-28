namespace ChatAppServer.WebAPI.Dtos
{
    public sealed record class SendMessageDto(
        Guid UserId,
        Guid ToUserId,
       String Message);
    

    
}
