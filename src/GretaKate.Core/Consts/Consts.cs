using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GretaKate.Core.Consts
{
    public sealed class ContentTypes
    {
        public const string Home = "umbHomePage";
        public const string Collection = "Collection";
        public const string Dress = "Dress";
    }

    namespace FieldNames
    {
        public sealed class Collection
        {
            public const string Title = "title";
            public const string Summary = "summary";
            public const string HeroImage = "heroImage";
        }

        public sealed class Dress
        {
            public const string Title = "title";
            public const string Description = "description";
            public const string HeroImage = "heroImage";
            public const string Price = "price";
            public const string OtherImages = "otherImages";
        }

        public sealed class Footer
        {
            public const string LocationHeading = "locationHeading";
            public const string Location = "location";
            public const string OpeningHoursHeading = "openingHoursHeading";
            public const string OpeningHours = "openingHours";
            public const string SocialHeading = "socialHeading";
            public const string FacebookLink = "facebookLink";
            public const string InstagramLink = "instagramLink";
            public const string PinterestLink = "pinterestLink";
            public const string YouTubeLink = "youTubeLink";
        }

        public sealed class Generic
        {
            public const string UmbracoFile = "umbracoFile";
        }
    }
}
