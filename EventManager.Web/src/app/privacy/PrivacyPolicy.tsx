"use client";
import React from "react";
import { Typography } from "@mui/material";
import { PageCard } from "../../components/PageCard";

export const PrivacyPolicy: React.FunctionComponent = () => {
  return (
    <>
      <PageCard>
        <Typography variant="h6">Information Collection and Use:</Typography>
        <br />
        <Typography component="p">
          Navodayan&apos;s App aka this website, collects registration, circulation qualification, and other information
          (including e-mail addresses) that you provide to us. This is done to help us provide our customers with the
          best customer service and valuable information regarding relevant products and services from Navodayan&apos;s App and
          appropriate third parties.
        </Typography>
      </PageCard>
      <PageCard>
        <Typography variant="h6">Information Sharing and Disclosure:</Typography>
        <br />
        <Typography component="p">
          Data may be used to update and improve our publications; as well as inform you of important industry news,
          events, services, and/or products. Occasionally, we may release the data that you provide to third parties
          that wish to market products/services that may be of interest to you. We have procedures in place to safeguard
          and help prevent unauthorized access, data security, and correct use of the information we collect on-line. We
          will not sell, share, or rent any financial information collected from you, except as necessary to fulfill
          your order. The views expressed in any Navodayan&apos;s App communication channel (example: portal, blog, newsletter,
          social sites), are those of the individual author, and not to be attributed to Navodayan&apos;s App, or of any person
          or organization affiliated with or otherwise doing business with Navodayan&apos;s App.
        </Typography>
      </PageCard>
      <PageCard>
        <Typography variant="h6">Cookies:</Typography>
        <br />
        <Typography component="p">
          Navodayan&apos;s App employs cookies to recognize registered users and their access privileges, as well as to track
          site usage and maintain security of the user&apos;s account. In some cases, subscribers who do not accept cookies
          from Navodayan&apos;s App may not be able to access advanced areas of publication sites. Additionally, third parties
          including &apos;Google Inc.&apos; may collect information through the use of cookies and web beacons.
        </Typography>
      </PageCard>
      <PageCard>
        <Typography variant="h6">Opt-out features:</Typography>
        <br />
        <Typography component="p">
          You will have opportunities to opt-out of receiving information from third parties not affiliated with Bipul&apos;s
          Web. Any communication that is sent to you will provide the opportunity to opt-out of future communications of
          the same type. Your information will also be used to send renewal requests from Navodayan&apos;s App publications and
          products to which you currently subscribe. To opt-out, please contact Navodayan&apos;s App in writing and let us know
          your specific opt-out request and it will be fulfilled as soon as possible. If you need assistance in updating
          the information you have provided to Navodayan&apos;s App or you have questions or comments about these policies,
          contact us at email above. Navodayan&apos;s App will review this privacy policy as needed and may update it
          periodically. By continued use of Navodayan&apos;s App content and products, you consent to the collection and use of
          your information by Navodayan&apos;s App and accept any changes to this policy.
        </Typography>
      </PageCard>
    </>
  );
};
