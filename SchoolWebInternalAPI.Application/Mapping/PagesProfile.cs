using AutoMapper;
using SchoolWebInternalAPI.Domain.Entities.PagesCSM;
using SchoolWebInternalAPI.Application.DTOs.Pages.Home;
using SchoolWebInternalAPI.Application.DTOs.Pages.History;
using SchoolWebInternalAPI.Application.DTOs.Pages.Mission;
using SchoolWebInternalAPI.Application.DTOs.Pages.Organization;
using SchoolWebInternalAPI.Application.DTOs.Pages.TeachersPage;
using SchoolWebInternalAPI.Application.DTOs.Pages.Links;
using SchoolWebInternalAPI.Application.DTOs.Pages.Contact;
using SchoolWebInternalAPI.Application.DTOs.Pages.Settings;
using SchoolWebInternalAPI.Application.DTOs.Pages.Footer;
using SchoolWebInternalAPI.Domain.Entities.Pages;

namespace SchoolWebInternalAPI.Application.Mapping
{
    public class PagesProfile : Profile
    {
        public PagesProfile()
        {
            // HOME
            CreateMap<HomePageContent, HomePageResponseDto>();
            CreateMap<HomePageUpdateDto, HomePageContent>();

            // HISTORY
            CreateMap<HistoryPageContent, HistoryPageResponseDto>();
            CreateMap<HistoryPageUpdateDto, HistoryPageContent>();

            // MISSION
            CreateMap<MissionPageContent, MissionPageResponseDto>();
            CreateMap<MissionPageUpdateDto, MissionPageContent>();

            // ORGANIZATION
            CreateMap<OrganizationPageContent, OrganizationPageResponseDto>();
            CreateMap<OrganizationPageUpdateDto, OrganizationPageContent>();

            // TEACHERS PAGE (NOT teachers)
            CreateMap<TeachersPageContent, TeachersPageResponseDto>();
            CreateMap<TeachersPageUpdateDto, TeachersPageContent>();

            // LINKS PAGE
            CreateMap<LinksPageContent, LinksPageResponseDto>();
            CreateMap<LinksPageUpdateDto, LinksPageContent>();

            // CONTACT PAGE
            CreateMap<ContactPageContent, ContactPageResponseDto>();
            CreateMap<ContactPageUpdateDto, ContactPageContent>();

            // SITE SETTINGS
            CreateMap<SiteSettings, SiteSettingsResponseDto>();
            CreateMap<SiteSettingsUpdateDto, SiteSettings>();

            // FOOTER
            CreateMap<FooterContent, FooterContentResponseDto>();
            CreateMap<FooterContentUpdateDto, FooterContent>();

            // HOME
            CreateMap<HomePage, HomePageResponseDto>();
            CreateMap<HomePageUpdateDto, HomePage>();

            // CONTACT
            CreateMap<ContactPage, ContactPageResponseDto>();
            CreateMap<ContactPageUpdateDto, ContactPage>();

            // FOOTER
            CreateMap<FooterPage, FooterContentResponseDto>();
            CreateMap<FooterContentUpdateDto, FooterPage>();

            CreateMap<HistoryPage, HistoryPageUpdateDto>().ReverseMap();

            CreateMap<LinksPage, LinksPageUpdateDto>().ReverseMap();

            CreateMap<MissionPage, MissionPageUpdateDto>().ReverseMap();

            CreateMap<OrganizationPage, OrganizationPageUpdateDto>().ReverseMap();

            CreateMap<SiteSettings, SiteSettingsUpdateDto>().ReverseMap();


        }
    }
}
