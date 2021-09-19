import React, { useState, useEffect } from 'react'
import { Container, Dropdown, Menu, Input } from 'semantic-ui-react'
//import data from "./testData/data.json";
import UserHeader from '../components/UserHeader';
import PortfolioDetails from '../components/UserProfile/PortfolioDetails';
import Footer from '../components/Footer';
import EditPortfolio from './EditPortfolio';


export default function UserProfileView() {

    const [data, setData] = useState([]);

    const [activePortfolio, setActivePortfolio] = useState(0);

    const [activeCompany, setActiveCompany] = useState(0);

    //replace with localStorage.getItem("id")
    const testUserId = "062399bc-fd17-415d-9eac-448b26f2ea2c";

    const url = `http://localhost:3010/api/Companies/portfolio?userId=${testUserId}`;


    useEffect(() => {
        fetch(url).then(result => {
            if (result.status != 200) {
                console.log("error");
                return;
            }
            result.json().then(data => {
                if (data != null) {
                    setData(data.result);
                }
            })
        })
    }, []);


    const handlePortfolioChange = (e, { value }) => {
        setActivePortfolio(value);
        setActiveCompany(0);
    };


    const handleCompanyChange = (e, { index }) => {
        setActiveCompany(index);
    };


    
    const portfolioNames = data.map((item, i) => {
        return { index: i, text: item.portfolioName, value: i }
    });

    const Greeting = () => {
        return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, (UserName)!</h1>
                    <a href="http://localhost:3010/user/edit">Edit my profile</a>
                </article>
            </section>
        );
    }


    const PortfolioDropdown = () => (
        <Dropdown
            placeholder='Select Portfolio'
            fluid
            selection
            clearable
            options={portfolioNames}
            onChange={handlePortfolioChange}
        />
    );


    const PortfolioCompanies = () => {
        if (data.length > 0) {
            return (
                <Menu secondary vertical>
                    {
                        data[activePortfolio].portfolioCompanies.map((item, i) => {
                            return (
                                <Menu.Item
                                    name={(item.name)}
                                    active={activeCompany === i}
                                    key={i}
                                    index={i}
                                    onClick={handleCompanyChange} />
                            );
                        })
                    }
                </Menu>
            );
        }

        return (
            <Menu secondary vertical>
                <Menu.Item
                    name="Loading..."
                    active="true"
                    index="1"
                />
            </Menu>
        );
    }

    const postNewPortfolio = (e) => {

        let request = fetch("localhost:3010/api/Companies/CreatePortfolio", { 
            method: "POST", 
            body: { 
                name: e.target.value 
            } 
        })

        console.log(e.target);

    };


    const AddPortfolio = () => {
        return <Input
            icon={{ name: 'plus', circular: true, link: true }}
            placeholder='Add Portfolio'
            onSubmit={() => console.log('test')} />;
    }


    const PortfolioBody = () => {
        if (data.length > 0) {
            return (
                <section className="portfolio-section five-vw-margin-lr">

                    <section className="portfolio-list">

                        <PortfolioDropdown />

                        <PortfolioCompanies data={data} />

                        <hr />

                        <a href={`http://localhost:3000/user/portfolio/edit/${data[activePortfolio].portfolioId}`}>Edit portfolio</a>

                        <hr />

                        <AddPortfolio />

                    </section> {/*end portfolio list*/}

                    <section className="portfolio-item-detail">

                        <PortfolioDetails data={data} activeCompany={activeCompany} activePortfolio={activePortfolio} className="detail-table" />

                    </section>

                </section>
            );

        }

        return (
            <h1>Loading...</h1>
        );
    }



    return (

        <div>
            <UserHeader />

            <Container className="profile">

                <Greeting />

                <PortfolioBody />
                

            </Container>

            <Footer />

        </div>
    )
}
