namespace SwissKip.Web.Queries
{
    using System.Linq;

    using Dapper;

    using SwissKip.Web.Models;
    using System.Collections.Generic;

    public class TokenQuery
    {
        private readonly int ownerId;

        public TokenQuery(int ownerId)
        {
            this.ownerId = ownerId;
        }

        public List<BasicInfoModel> ExecuteNew()
        {
            var owners = Current.Connection.Query<BasicInfoModel>(
                "Select u.Id, u.FirstName + ' ' + u.LastName as FullName, u.Email, u.IconId, i.Description Description1, u.ColourId, c.Description Description2, u.SecretPhrase, u.TokenNumber  from [dbo].[User] u " +
                "inner join [dbo].[Icon] i on u.IconId=i.Id  " +
                "inner join [dbo].[Colour] c on u.ColourId=c.Id " +
                "where u.Id=@ownerId", new { ownerId }).ToList();
            return owners;
        }
    }
}