import React, { useState, useEffect } from 'react'
import { Image, List, Container, ListContent, Button, Checkbox } from 'semantic-ui-react';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';
import { useParams } from 'react-router';
import { forInStatement } from '@babel/types';



export default function EditPortfolio() {

    const [portfolio, setPortfolio] = useState({ portfolioName: "" });

    const [companies, setCompanies] = useState([]);

    const [toDelete, setToDelete] = useState([]);

  const id = useParams();



    useEffect(() => {
        /* await fetch(`http://localhost:3010/api/Portfolios/portfolioCompanies?portfolioId=${id.id}`)
    
          .then(result => {
            if (result.status != 200) {
              console.log("error");
              return;
            }
            return result.json()
          })
    
          .then(data => {
            if (data != null) {
    
              setPortfolio(() => data.result);
              setCompanies(() => portfolio.portfolioCompanies);
              ready = true;
              console.log(portfolio)
            }
          }) */

        (async function () {
            try {
                const response = await fetch(`http://localhost:3010/api/Portfolios/portfolioCompanies?portfolioId=${id.id}`)

                const data = await response.json();

                if (data != null) {
                    setPortfolio(() => data.result);
                    setCompanies(() => data.result.portfolioCompanies);
                    console.log(data.result)
                }

            } catch (e) {
                console.error(e);
            }
        })()


    }, []);

    const body = {
        'Tickers': toDelete,
        'PortfolioName': `${portfolio.portfolioName}`,
        'Uuid': `${id.id}`
    }
    console.log(body)
    async function buttonHandlerSave() {
        var txt;
        var r = window.confirm("Press ok if you wish to save the folder!");
        if (r == true) {
            var result = await fetch(`http://localhost:3010/api/Portfolios/updateportfolio`, {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(body)
            }).then(resp => resp.json());
            console.log(result);
            if (result.ok) {
                txt = "Done!";
            }
            else {
                txt = "Failed!";
            }
        } else {
            txt = "You Canceled!";
        }
        //document.getElementById("demo").innerHTML = txt;

    }
    console.log(id.id);
    async function buttonHandlerDelete() {
        var txt;
        var r = window.confirm("Press ok if you wish to delete the folder!");
        if (r == true) {
            var result = await fetch(`http://localhost:3010/api/Portfolios/deletePortfolio?Id=${id.id}`, {
                method: "DELETE",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            });
            if (result.ok) {
                txt = "Done!";
            }
            else {
                txt = "Failed!";
            }
        } else {
            txt = "You Canceled!";
        }
        //document.getElementById("demo").innerHTML = txt;

    }


    const handleCheckbox = (e, { checked, companyTicker }) => {

        if (checked) {
            setToDelete(() => [...toDelete, companyTicker]);
        } else {
            setToDelete(() => toDelete.filter(item => item !== companyTicker));
        }

        //console.log('items to delete: ', toDelete, checked);

    };

    const handleRemoveChecked = () => {


        setCompanies(() => companies.filter((item) => !(toDelete.includes(item.ticker))));
    }

    const handleRemoveItem = (e, { companyTicker }) => {

    setCompanies(() => companies.filter(item => item.ticker !== companyTicker));
    setToDelete(() => [...toDelete, companyTicker]);

        //console.log('companies: ', companies, 'to delete: ', toDelete)

    };

    const handleOnChange = (e) => {

        setPortfolio((prevState) => ({ ...prevState, portfolioName: e.target.value }));
    }


    return (<>

        <div>
            <UserHeader />
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, (UserName)!</h1>
                    <input onChange={handleOnChange} value={portfolio.portfolioName} /> <button onClick={buttonHandlerDelete}  >delete Portfolio</button>

          <h1 id="demo"></h1>
        </article>
      </section>
      <Container>
        {toDelete.length > 0 && <Button onClick={handleRemoveChecked}>Delete selected</Button>}
        {companies.length > 0 &&
            (<List celled>
              {companies.map((item, index) =>
                <List.Item key={index}>
                  <List.Content floated="right">
                    <Button icon="trash alternate" size="medium" companyTicker={item.ticker} onClick={handleRemoveItem} />
                  </List.Content>
                  <List.Content>
                    <List.Header>{item.ticker}</List.Header>
                    <p>{item.name}</p>
                    <Checkbox label='Selected' onChange={handleCheckbox} companyTicker={item.ticker} checked={toDelete.includes(item.ticker)} />
                  </List.Content>
                </List.Item>
              )}
            </List>)
        }
        <Container textAlign="center">
          <Button onClick={buttonHandlerSave}>Save</Button>
          <a href={`http://localhost:3000/user/profile`}>Cancel</a>
        </Container>
      </Container>

        </div>
    </>);
}