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

            CreateMap<HomePageUpdateDto, HomePage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore())
                .ForMember(d => d.IsPublished, opt => opt.Ignore())
                .ForMember(d => d.LastUpdatedAt, opt => opt.Ignore())
                .ForMember(d => d.LastUpdatedBy, opt => opt.Ignore());


            // -----------------------------------------
            // HISTORY
            // -----------------------------------------
            CreateMap<HistoryPage, HistoryPageResponseDto>();

            CreateMap<HistoryPageUpdateDto, HistoryPage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // MISSION
            // -----------------------------------------
            CreateMap<MissionPage, MissionPageResponseDto>();

            CreateMap<MissionPageUpdateDto, MissionPage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // ORGANIZATION
            // -----------------------------------------
            CreateMap<OrganizationPage, OrganizationPageResponseDto>();

            CreateMap<OrganizationPageUpdateDto, OrganizationPage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // LINKS PAGE
            // -----------------------------------------
            CreateMap<LinksPage, LinksPageResponseDto>();

            CreateMap<LinksPageUpdateDto, LinksPage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // CONTACT PAGE
            // -----------------------------------------
            CreateMap<ContactPage, ContactPageResponseDto>();

            CreateMap<ContactPageUpdateDto, ContactPage>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedBy, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // SITE SETTINGS
            // -----------------------------------------
            CreateMap<SiteSettings, SiteSettingsResponseDto>();

            CreateMap<SiteSettingsUpdateDto, SiteSettings>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());


            // -----------------------------------------
            // FOOTER
            // -----------------------------------------
            CreateMap<FooterContent, FooterContentResponseDto>();

            CreateMap<FooterContentUpdateDto, FooterContent>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.CreatedAt, opt => opt.Ignore())
                .ForMember(d => d.UpdatedAt, opt => opt.Ignore());
        }
    }
}
