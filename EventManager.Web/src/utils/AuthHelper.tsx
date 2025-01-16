import React, { PropsWithChildren, useContext, useEffect, useMemo, useState } from "react"
import { AuthContext, AuthData } from "./AuthContextHelper"
import { IsEmptyObj } from "./CommonHelper"
import { IsTokenValid } from "./TokenHelper"

export const AuthContextProvider = ({ children }: PropsWithChildren) => {
  const [authData, setAuthData] = useState({} as AuthData)
  const [isAuthenticated, setIsAuthenticated] = useState(false)

  useEffect(() => {
    setIsAuthenticated(IsTokenValid())
  }, [])

  useEffect(() => {
    if (!IsEmptyObj(authData)) { setIsAuthenticated(true) }
  }, [authData])

  useEffect(() => {
    if (!isAuthenticated) { setAuthData({} as AuthData) }
  }, [isAuthenticated])

  const contextValue = useMemo(() => ({
    isAuthenticated,
    setIsAuthenticated,
    authData,
    setAuthData
  }), [isAuthenticated, setIsAuthenticated, authData, setAuthData])

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  )
}

export const useAuth = (): boolean => {
  const { isAuthenticated } = useContext(AuthContext)
  return isAuthenticated
}