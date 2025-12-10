using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Extensions;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Bit2Sky.Bit2EHR.Auditing.Dto;
using Bit2Sky.Bit2EHR.Authorization.Accounts.Dto;
using Bit2Sky.Bit2EHR.Authorization.Delegation;
using Bit2Sky.Bit2EHR.Authorization.Permissions.Dto;
using Bit2Sky.Bit2EHR.Authorization.Roles;
using Bit2Sky.Bit2EHR.Authorization.Roles.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users;
using Bit2Sky.Bit2EHR.Authorization.Users.Delegation.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Importing.Dto;
using Bit2Sky.Bit2EHR.Authorization.Users.Profile.Dto;
using Bit2Sky.Bit2EHR.Chat;
using Bit2Sky.Bit2EHR.Chat.Dto;
using Bit2Sky.Bit2EHR.Common.Dto;
using Bit2Sky.Bit2EHR.DynamicEntityProperties.Dto;
using Bit2Sky.Bit2EHR.Editions;
using Bit2Sky.Bit2EHR.Editions.Dto;
using Bit2Sky.Bit2EHR.EntityChanges;
using Bit2Sky.Bit2EHR.EntityChanges.Dto;
using Bit2Sky.Bit2EHR.Friendships;
using Bit2Sky.Bit2EHR.Friendships.Cache;
using Bit2Sky.Bit2EHR.Friendships.Dto;
using Bit2Sky.Bit2EHR.Localization.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy;
using Bit2Sky.Bit2EHR.MultiTenancy.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.HostDashboard.Dto;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments;
using Bit2Sky.Bit2EHR.MultiTenancy.Payments.Dto;
using Bit2Sky.Bit2EHR.Notifications.Dto;
using Bit2Sky.Bit2EHR.Organizations.Dto;
using Bit2Sky.Bit2EHR.Sessions.Dto;
using Bit2Sky.Bit2EHR.WebHooks.Dto;

namespace Bit2Sky.Bit2EHR;

internal static class CustomDtoMapper
{
    public static void CreateMappings(IMapperConfigurationExpression configuration)
    {
        //Inputs
        configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
        configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
        configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
        configuration.CreateMap<IInputType, FeatureInputTypeDto>()
            .Include<CheckboxInputType, FeatureInputTypeDto>()
            .Include<SingleLineStringInputType, FeatureInputTypeDto>()
            .Include<ComboboxInputType, FeatureInputTypeDto>();
        configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
        configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
            .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
        configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
        configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
            .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

        //Chat
        configuration.CreateMap<ChatMessage, ChatMessageDto>();
        configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

        //Feature
        configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
        configuration.CreateMap<Feature, FlatFeatureDto>();

        //Role
        configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
        configuration.CreateMap<Role, RoleListDto>();
        configuration.CreateMap<UserRole, UserListRoleDto>();


        //Edition
        configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
        configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
        configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
        configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

        configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

        configuration.CreateMap<SubscribableEdition, EditionListDto>();
        configuration.CreateMap<Edition, EditionEditDto>();
        configuration.CreateMap<Edition, SubscribableEdition>();
        configuration.CreateMap<Edition, EditionSelectDto>();


        //Payment
        configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>()
            .ReverseMap()
            .ForMember(dto => dto.TotalAmount, options => options.MapFrom(e => e.GetTotalAmount()));
        configuration.CreateMap<SubscriptionPaymentProductDto, SubscriptionPaymentProduct>().ReverseMap();
        configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
        configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

        //Permission
        configuration.CreateMap<Permission, FlatPermissionDto>();
        configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

        //Language
        configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
        configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
        configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
        configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
            .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

        //Tenant
        configuration.CreateMap<Tenant, RecentTenant>();
        configuration.CreateMap<Tenant, TenantLoginInfoDto>();
        configuration.CreateMap<Tenant, TenantListDto>();
        configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
        configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

        //User
        configuration.CreateMap<User, UserEditDto>()
            .ForMember(dto => dto.Password, options => options.Ignore())
            .ReverseMap()
            .ForMember(user => user.Password, options => options.Ignore());
        configuration.CreateMap<User, UserLoginInfoDto>();
        configuration.CreateMap<User, UserListDto>();
        configuration.CreateMap<User, ChatUserDto>();
        configuration.CreateMap<User, OrganizationUnitUserListDto>();
        configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
        configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
        configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
        configuration.CreateMap<ImportUserDto, User>().ForMember(x => x.Roles, options => options.Ignore());
        configuration.CreateMap<User, FindUsersOutputDto>();
        configuration.CreateMap<User, FindOrganizationUnitUsersOutputDto>();

        //AuditLog
        configuration.CreateMap<AuditLog, AuditLogListDto>();

        //EntityChanges
        configuration.CreateMap<EntityChange, EntityChangeListDto>();
        configuration.CreateMap<EntityChange, EntityAndPropertyChangeListDto>();
        configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
        configuration.CreateMap<EntityChangePropertyAndUser, EntityChangeListDto>();

        //Friendship
        configuration.CreateMap<Friendship, FriendDto>();
        configuration.CreateMap<FriendCacheItem, FriendDto>();

        //OrganizationUnit
        configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

        //Webhooks
        configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
        configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
            .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                options => options.MapFrom(l => l.WebhookEvent.WebhookName))
            .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                options => options.MapFrom(l => l.WebhookEvent.Data));

        configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

        configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
        configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
        configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
            .ForMember(dto => dto.DynamicPropertyName,
                options => options.MapFrom(entity =>
                    entity.DynamicProperty.DisplayName.IsNullOrEmpty()
                        ? entity.DynamicProperty.PropertyName
                        : entity.DynamicProperty.DisplayName));
        configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

        configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

        //User Delegations
        configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

        /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
    }
}
