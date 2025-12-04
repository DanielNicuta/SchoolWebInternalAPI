using AutoMapper;
using SchoolWebInternalAPI.Domain.Entities.Pages;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;

namespace SchoolWebInternalAPI.Application.Mapping
{
    public class PagesProfile : Profile
    {
        public PagesProfile()
        {
            // -----------------------------------------
            // HOME
            // -----------------------------------------
            CreateMap<HomePage, HomePageResponseDto>();
            CreateMap<HomePageUpdateDto, HomePage>();

            // -----------------------------------------
            // HISTORY
            // -----------------------------------------
            CreateMap<HistoryPage, HistoryPageResponseDto>();
            CreateMap<HistoryPageUpdateDto, HistoryPage>();

            // -----------------------------------------
            // MISSION
            // -----------------------------------------
            CreateMap<MissionPage, MissionPageResponseDto>();
            CreateMap<MissionPageUpdateDto, MissionPage>();

            // -----------------------------------------
            // ORGANIZATION
            // -----------------------------------------
            CreateMap<OrganizationPage, OrganizationPageResponseDto>();
            CreateMap<OrganizationPageUpdateDto, OrganizationPage>();

            // -----------------------------------------
            // LINKS PAGE
            // -----------------------------------------
            CreateMap<LinksPage, LinksPageResponseDto>();
            CreateMap<LinksPageUpdateDto, LinksPage>();

            // -----------------------------------------
            // CONTACT PAGE
            // -----------------------------------------
            CreateMap<ContactPage, ContactPageResponseDto>();
            CreateMap<ContactPageUpdateDto, ContactPage>();

            // -----------------------------------------
            // SITE SETTINGS
            // -----------------------------------------
            CreateMap<SiteSettings, SiteSettingsResponseDto>();
            CreateMap<SiteSettingsUpdateDto, SiteSettings>();

            // -----------------------------------------
            // FOOTER
            // -----------------------------------------
            CreateMap<FooterContent, FooterContentResponseDto>();
            CreateMap<FooterContentUpdateDto, FooterContent>();
        }
    }
}
