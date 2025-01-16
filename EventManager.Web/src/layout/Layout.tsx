"use client";
import React, { useState, useEffect } from 'react';
import { Box, useMediaQuery } from '@mui/material';
import Grid from '@mui/material/Grid2';
import { useTheme } from '@mui/material/styles';
import { Sidebar } from './child/Sidebar/Sidebar';
import { Footer } from './child/Footer';
import { Topbar } from './child/Topbar';
import { BottomNavPages, footerProps, topbarProps } from './LayoutConfig';
import { useAuth } from '@/utils/AuthHelper';
import { RightColumn } from './child/RightColumn';
import { MobileFooterNav } from './child/MobileFooterNav';

const rootStyle: React.CSSProperties = {
  padding: 0,
  fontFamily: 'Roboto, Helvetica, Arial, sans-serif',
  height: '100%',
  backgroundColor: '#f5f5f5'
};

const layoutDesktopStyle: React.CSSProperties = {
  paddingTop: 55,
  height: '100%',
  paddingLeft: 280
};

const layoutDesktopUnAuthStyle: React.CSSProperties = {
  paddingTop: 55,
  height: '100%'
};

const layoutMobileStyle: React.CSSProperties = {
  paddingTop: 55,
  height: '100%',
  paddingLeft: 0
};

const mainStyle: React.CSSProperties = {
  minHeight: `calc(100dvh - 55px)`,
  margin: '0px 5% 0px 5%',
  display: 'flex',
  flexDirection: 'column'
};

const centerMainStyle: React.CSSProperties = {
  flex: 1
};

const footerStyle: React.CSSProperties = {
  textAlign: 'center'
};

export interface LayoutProps {
  children: React.ReactNode;
}

export const Layout: React.FC<LayoutProps> = (props) => {
  const theme = useTheme();
  const [openSidebar, setOpenSidebar] = useState(false);
  // const isDesktop = useMediaQuery(theme.breakpoints.up('lg'), {
  //   defaultMatches: true
  // });

  const isMobile = useMediaQuery(theme.breakpoints.down('sm'), {
    defaultMatches: true
  });

  const isLargeScreen = useMediaQuery(theme.breakpoints.up('lg'), {
    defaultMatches: true
  });

  useEffect(() => {
    if (isMobile) {
      setOpenSidebar(false);
    }
    else {
      setOpenSidebar(true);
    }
  }, [isMobile]);

  const handleSidebarOpen = () => {
    setOpenSidebar(true);
  };

  const handleSidebarClose = () => {
    setOpenSidebar(false);
  };

  const shouldOpenSidebar = isMobile ? openSidebar : true;
  const isAuthenticated = useAuth();

  return (
    <>
      <div style={rootStyle}>
        <div style={isAuthenticated ? (isMobile ? layoutMobileStyle : layoutDesktopStyle) : (isMobile ? layoutMobileStyle : layoutDesktopUnAuthStyle)}>
          <Topbar
            onSidebarOpen={handleSidebarOpen}
            logoUrl={topbarProps.logoUrl}
            siteTitle={topbarProps.siteTitle}
          />
          {isAuthenticated && (
            <Sidebar
              onClose={handleSidebarClose}
              open={shouldOpenSidebar}
              variant={isMobile ? 'temporary' : 'persistent'}
            />
          )}
          <div style={mainStyle}>
            <div style={centerMainStyle}>
              {!isMobile && isAuthenticated ? (
                <Grid container spacing={8}>
                  {
                    isLargeScreen ? (
                      <>
                        <Grid size={{ xs: 8 }}>
                          <div>{props.children}</div>
                        </Grid>
                        <Grid size={{ xs: 4 }}>
                          <Box position="sticky" flex={1}>
                            <RightColumn />
                          </Box>
                        </Grid>
                      </>
                    ) : (
                      <>
                        <Grid size={{ xs: 12 }}>
                          <div>{props.children}</div>
                        </Grid>
                      </>
                    )
                  }
                </Grid>
              ) : (
                <div style={centerMainStyle}>{props.children}</div>
              )}
            </div>
            <div style={footerStyle}>
              <div>
                {
                  isMobile && isAuthenticated && <MobileFooterNav pages={BottomNavPages} onSidebarOpen={handleSidebarOpen} />
                }
              </div>
              <Footer {...footerProps} />
            </div>
          </div>
        </div>
      </div>
    </>

  );
};
