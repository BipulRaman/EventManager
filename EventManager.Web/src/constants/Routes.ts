export enum Pathname {
    Root = '/',
    About = '/about',
    Newsfeed = '/newsfeed',
    Newsfeed_Manage = '/newsfeed/manage',
    Newsfeed_Create = '/newsfeed/create',
    Profile = '/profile',
    Profile_Edit = '/profile/edit',    
    NearBy = '/nearby',
    NearBy_Business = '/nearby/business',
    NearBy_Medico = '/nearby/medico',    
    Business = '/business',
    Business_Add = '/business/add',
    Business_Edit = '/business/edit',
    Expense = '/expense',
    Expense_Add = '/expense/add',
    CheckIn = '/checkin',
    Notifications = '/notifications',
    Events = '/events',
    Mentorship = '/mentorship',
    Poll = '/poll',
    Identity = '/identity',
    Identity_Verify = '/identity/verify',
    Security = '/security',
    Security_Reset = '/security/reset',
    NotFound = '/404',
    Expired = '/expired',
    Privacy = '/privacy',
    Disclaimer = '/disclaimer',
    License = '/license',
    Register = '/register',
    Login = '/login',
    Logout = '/logout',
    Default = '*'
}

export const HomeTabs = [
    { label: 'Home', path: Pathname.Root }
];

export const ProfileTabs = [
    { label: 'Profile', path: Pathname.Profile },
    { label: 'Update', path: Pathname.Profile_Edit },
];

export const BusinessTabs = [
    { label: 'My Business', path: Pathname.Business },
    { label: 'Add', path: Pathname.Business_Add },
];

export const NearByTabs = [
    { label: 'Alumni', path: Pathname.NearBy },
    { label: 'Business', path: Pathname.NearBy_Business },
    { label: 'Medico', path: Pathname.NearBy_Medico },
];

export const IdentityTabs = [
    { label: 'Identity', path: Pathname.Identity },
    { label: 'Verify', path: Pathname.Identity_Verify },
];

export const SecurityTabs = [
    { label: 'Security', path: Pathname.Security },
    { label: 'Reset Auth', path: Pathname.Security_Reset },
];

export const NewsfeedTabs = [
    { label: 'Feed', path: Pathname.Newsfeed },
    { label: 'Manage', path: Pathname.Newsfeed_Manage },
    { label: 'Create', path: Pathname.Newsfeed_Create },
];

export const ExpenseTabs = [
    { label: 'Expense', path: Pathname.Expense },
    { label: 'Add', path: Pathname.Expense_Add },
];

export const CheckInTabs = [
    { label: 'Check-In', path: Pathname.CheckIn },
];

export const RoutesMetadata = [
    { title: 'Home | Samagam', route: Pathname.Root, description: 'The home page of Samagam.' },
    { title: 'Newsfeed | Samagam', route: Pathname.Newsfeed, description: 'The newsfeed page of Samagam.' },
    { title: 'Profile | Samagam', route: Pathname.Profile, description: 'The profile page of Samagam.' },
    { title: 'Profile Edit | Samagam', route: Pathname.Profile_Edit, description: 'The profile edit page of Samagam.' },
    { title: 'NearBy Alumni | Samagam', route: Pathname.NearBy, description: 'The nearby page of Samagam.' },
    { title: 'NearBy Business | Samagam', route: Pathname.NearBy_Business, description: 'The nearby business page of Samagam.' },
    { title: 'NearBy Medico | Samagam', route: Pathname.NearBy_Medico, description: 'The nearby medico page of Samagam.' },
    { title: 'My Business | Samagam', route: Pathname.Business, description: 'The business page of Samagam.' },
    { title: 'Business Add | Samagam', route: Pathname.Business_Add, description: 'The business add page of Samagam.' },
    { title: 'Business Edit | Samagam', route: Pathname.Business_Edit, description: 'The business edit page of Samagam.' },
    { title: 'Notifications | Samagam', route: Pathname.Notifications, description: 'The notifications page of Samagam.' },
    { title: 'Events | Samagam', route: Pathname.Events, description: 'The events page of Samagam.' },
    { title: 'Expense | Samagam', route: Pathname.Expense, description: 'The expense page of Samagam.' },
    { title: 'Expense Add | Samagam', route: Pathname.Expense_Add, description: 'The expense add page of Samagam.' },
    { title: 'Check-In | Samagam', route: Pathname.CheckIn, description: 'The check-in page of Samagam.' },
    { title: 'Mentorship | Samagam', route: Pathname.Mentorship, description: 'The mentorship page of Samagam.' },
    { title: 'Poll | Samagam', route: Pathname.Poll, description: 'The poll page of Samagam.' },
    { title: 'Identity | Samagam', route: Pathname.Identity, description: 'The identity page of Samagam.' },
    { title: 'Security | Samagam', route: Pathname.Security, description: 'The security page of Samagam.' },
    { title: 'Auth Reset | Samagam', route: Pathname.Security_Reset, description: 'The auth reset page of Samagam.' },
    { title: '404 | Samagam', route: Pathname.NotFound, description: 'The 404 page of Samagam.' },
    { title: 'Expired | Samagam', route: Pathname.Expired, description: 'The expired page of Samagam.' },
    { title: 'Privacy | Samagam', route: Pathname.Privacy, description: 'The privacy page of Samagam.' },
    { title: 'Disclaimer | Samagam', route: Pathname.Disclaimer, description: 'The disclaimer page of Samagam.' },
    { title: 'License | Samagam', route: Pathname.License, description: 'The license page of Samagam.' },
    { title: 'Register | Samagam', route: Pathname.Register, description: 'The register page of Samagam.' },
    { title: 'Login | Samagam', route: Pathname.Login, description: 'The login page of Samagam.' },
    { title: 'Logout | Samagam', route: Pathname.Logout, description: 'The logout page of Samagam.' },
    { title: 'Default | Samagam', route: Pathname.Default, description: 'The default page of Samagam.' },
];