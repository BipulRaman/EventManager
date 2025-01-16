import { Dispatch, SetStateAction, createContext } from 'react'

export type AuthData = {
	name: string
	email: string
}
export type AuthContextData = {
	authData: AuthData
	isAuthenticated: boolean
	setIsAuthenticated: Dispatch<SetStateAction<boolean>>
	setAuthData: Dispatch<SetStateAction<AuthData>>
}

export const AuthContext = createContext<AuthContextData>({
	authData: { name: '', email: '' },
	isAuthenticated: false,
	setIsAuthenticated: () => {},
	setAuthData: () => {}
})
