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
    { title: 'Home | Navodaya Alumni App', route: Pathname.Root, description: 'The Navodaya Alumni App.' },
    { title: 'Newsfeed | Navodaya Alumni App', route: Pathname.Newsfeed, description: 'The newsfeed of Navodaya Alumni App.' },
    { title: 'Profile | Navodaya Alumni App', route: Pathname.Profile, description: 'The profile of Navodaya Alumni App.' },
    { title: 'Profile Edit | Navodaya Alumni App', route: Pathname.Profile_Edit, description: 'The profile edit of Navodaya Alumni App.' },
    { title: 'NearBy Alumni | Navodaya Alumni App', route: Pathname.NearBy, description: 'The nearby of Navodaya Alumni App.' },
    { title: 'NearBy Business | Navodaya Alumni App', route: Pathname.NearBy_Business, description: 'The nearby business of Navodaya Alumni App.' },
    { title: 'NearBy Medico | Navodaya Alumni App', route: Pathname.NearBy_Medico, description: 'The nearby medico of Navodaya Alumni App.' },
    { title: 'My Business | Navodaya Alumni App', route: Pathname.Business, description: 'The business of Navodaya Alumni App.' },
    { title: 'Business Add | Navodaya Alumni App', route: Pathname.Business_Add, description: 'The business add of Navodaya Alumni App.' },
    { title: 'Business Edit | Navodaya Alumni App', route: Pathname.Business_Edit, description: 'The business edit of Navodaya Alumni App.' },
    { title: 'Notifications | Navodaya Alumni App', route: Pathname.Notifications, description: 'The notifications of Navodaya Alumni App.' },
    { title: 'Events | Navodaya Alumni App', route: Pathname.Events, description: 'The events of Navodaya Alumni App.' },
    { title: 'Expense | Navodaya Alumni App', route: Pathname.Expense, description: 'The expense of Navodaya Alumni App.' },
    { title: 'Mentorship | Navodaya Alumni App', route: Pathname.Mentorship, description: 'The mentorship of Navodaya Alumni App.' },
    { title: 'Poll | Navodaya Alumni App', route: Pathname.Poll, description: 'The poll of Navodaya Alumni App.' },
    { title: 'Identity | Navodaya Alumni App', route: Pathname.Identity, description: 'The identity of Navodaya Alumni App.' },
    { title: 'Security | Navodaya Alumni App', route: Pathname.Security, description: 'The security of Navodaya Alumni App.' },
    { title: 'Auth Reset | Navodaya Alumni App', route: Pathname.Security_Reset, description: 'The auth reset of Navodaya Alumni App.' },
    { title: '404 | Navodaya Alumni App', route: Pathname.NotFound, description: 'The 404 of Navodaya Alumni App.' },
    { title: 'Expired | Navodaya Alumni App', route: Pathname.Expired, description: 'The expired of Navodaya Alumni App.' },
    { title: 'Privacy | Navodaya Alumni App', route: Pathname.Privacy, description: 'The privacy of Navodaya Alumni App.' },
    { title: 'Disclaimer | Navodaya Alumni App', route: Pathname.Disclaimer, description: 'The disclaimer of Navodaya Alumni App.' },
    { title: 'License | Navodaya Alumni App', route: Pathname.License, description: 'The license of Navodaya Alumni App.' },
    { title: 'Register | Navodaya Alumni App', route: Pathname.Register, description: 'The register of Navodaya Alumni App.' },
    { title: 'Login | Navodaya Alumni App', route: Pathname.Login, description: 'The login of Navodaya Alumni App.' },
    { title: 'Logout | Navodaya Alumni App', route: Pathname.Logout, description: 'The logout of Navodaya Alumni App.' },
    { title: 'Default | Navodaya Alumni App', route: Pathname.Default, description: 'The default of Navodaya Alumni App.' },
];