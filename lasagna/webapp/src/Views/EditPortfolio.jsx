import React, { useState , useEffect} from 'react'
import { Image, List, Container, ListContent, Button, Checkbox } from 'semantic-ui-react';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';
import { useParams } from 'react-router';



export default function EditPortfolio( ) {
  
  const [portfolio, setPortfolio] = useState([]);
 
  const id= useParams();
  let testId= "D9F7672B-ACB8-401B-8A8A-5577C74A2855"
  
  useEffect(() => {
    fetch(`http://localhost:3010/api/companies/editportfolio?Id=${id}`).then(result => {
      if (result.status != 200) {
        console.log("error");
        return;
      }
      result.json().then(data => {
        if (data != null) {
          console.log(data.result);
          setPortfolio(data.result);
        }
      })
    })
  }, []);


  const Greeting = () => {
    return (
      <section className="greeting">
        <article className="avatar">
          <img src="../blank-avatar-sm.jpg" alt="" />
        </article>

        <article>
          <h1>Hello, (UserName)!</h1>
          {/* portfolio name */}
          <input defaultValue='name of portfolio' /> <button  >delete Portfolio</button>

        </article>
      </section>
    );
  }

  return (
    <div>

      <UserHeader />
      <Greeting />
      <Container>
        <List celled>
          {portfolio.map((item, index) => {
            <List.Item key={index}>
              <List.Content floated="right">
                <Button icon="trash alternate" size="medium" />
              </List.Content>
              <List.Content>
                <List.Header>{item.ticker}</List.Header>
                <p>{item.name}</p>
                <Checkbox label='Delete' />

              </List.Content>
            </List.Item>
          })
          }
          <List.Item>
            <Image avatar src='/images/avatar/small/daniel.jpg' />
            <List.Content>
              <List.Header>Poodle</List.Header>A poodle, it's pretty basic
            </List.Content>
          </List.Item>
          <List.Item>
            <Image avatar src='/images/avatar/small/daniel.jpg' />
            <List.Content>
              <List.Header>Paulo</List.Header>
              He's also a dog
            </List.Content>
          </List.Item>
        </List><Container textAlign="center">
          <Button >Save</Button>
          <Button >Cancel</Button>
        </Container>
      </Container>

    </div>
  )
}

