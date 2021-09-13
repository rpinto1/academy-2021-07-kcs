import React from 'react';
import Footer from '../components/Footer';
import BodyCompanyProfile from '../components/CompanyProfile/BodyCompanyProfile';
import Header from '../components/Header';
import { useParams } from 'react-router';

export default function CompanyProfileView() {

    const companyInfo = useParams();
 

    return (
        <div>
            
            <Header />

            <BodyCompanyProfile companyInfo={companyInfo} />

            <Footer />
        </div>
    )
}
