import React, { useState, useEffect,ReactDOM } from 'react'
import { Image, List, Container, ListContent, Button, Checkbox } from 'semantic-ui-react';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';
import { useParams } from 'react-router';



export default function EditPortfolio() {

  const [portfolio, setPortfolio] = useState([]);

  const id = useParams();

  useEffect(() => {
    fetch(`http://localhost:3010/api/companies/GetPortfolio?Id=${id.id}`).then(result => {
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

  async function buttonHandlerDelete() {
    var txt;
    var r = Window.confirm("Press ok if you wish to delete the folder!");
    if (r == true) {
      var result = await fetch(`http://localhost:3010/api/companies/deletePortfolio?Id=${id.id}`, {
        method: "DELETE",
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

  async function buttonHandlerSave() {
    var txt;
    var r = Window.confirm("Press ok if you wish to save the folder!");
    if (r == true) {
      var result = await fetch(`http://localhost:3010/api/companies/updatePortfolio?Id=${id.id}`, {
        method: "POST",
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
          <input defaultValue='name of portfolio' /> <button onClick={buttonHandlerDelete} >delete Portfolio</button>
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
            <Button onclick={buttonHandlerSave}>Save</Button>
            <Button >Cancel</Button>
          </Container>
        </Container>

      </div>
          </>);
}