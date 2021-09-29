import React from 'react';
import Footer from '../components/Footer';
import BodyCompanyProfile from '../components/CompanyProfile/BodyCompanyProfile';
import Header from '../components/Header';
import { useParams } from 'react-router';
import { userId } from '../components/UserManager';
import UserHeader from '../components/UserHeader';

export default function CompanyProfileView() {

    const companyInfo = useParams();
 

    return (
        <div>
            {
                !userId && <Header />
            }
            {
                userId && <UserHeader />
            }

            <BodyCompanyProfile companyInfo={companyInfo} />

            <Footer />
        </div>
    )
}
