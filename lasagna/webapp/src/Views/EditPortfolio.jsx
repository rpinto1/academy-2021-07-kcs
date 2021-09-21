import React, { useState, useEffect,ReactDOM } from 'react'
import { Image, List, Container, ListContent, Button, Checkbox } from 'semantic-ui-react';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';
import { useParams } from 'react-router';



export default function EditPortfolio() {

  const [portfolio, setPortfolio] = useState([]);

  const id = useParams();

  useEffect(() => {
    fetch(`http://localhost:3010/api/companies/editportfolio?Id=${id.id}`).then(result => {
      if (result.status != 200) {
        console.log("error");
        return;
      }
      result.json().then(data => {
        if (data != null) {
          setPortfolio(data.result);
        }
      })
    })
  }, []);

  async function myFunction(event,url,edit) {
    var txt;
    var r = confirm("Press a button!");
    if (r == true) {
      var result = await fetch(url, {
        method: edit,
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }       });
        if(result.ok){
          txt = "Done!";
        }
        else {
          txt = "Failed!";
        }
    } else {
      txt = "You Canceled!";
    }
    document.getElementById("demo").innerHTML = txt;
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
          <input defaultValue='name of portfolio' /> <button onClick={myFunction} >delete Portfolio</button>
        </article>
      </section>
        <Container>
          <List celled>
            {portfolio.map((item, index) => 
              <List.Item key={index}>
              <List.Content floated="right">
                <Button icon="trash alternate" size="medium" />
              </List.Content>
              <List.Content>
                <List.Header>{item.ticker}</List.Header>
                <p>{item.name}</p>
                <Checkbox label='Selected' />
              </List.Content>
            </List.Item>
            )}
          </List>
          <Container textAlign="center">
            <Button onclick={myFunction}>Save</Button>
            <Button >Cancel</Button>
          </Container>
        </Container>

      </div>
          </>);
}