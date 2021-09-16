import React,{useState} from 'react'
import { Image, List, Container, ListContent, Button, Checkbox } from 'semantic-ui-react';
import Header from '../components/Header';
import UserHeader from '../components/UserHeader';



export default function EditPortfolio(props) {
  const [portfolio, setPortfolio] = useState([]);

    const Greeting = () => {
        return (
            <section className="greeting">
                <article className="avatar">
                    <img src="../blank-avatar-sm.jpg" alt="" />
                </article>

                <article>
                    <h1>Hello, (UserName)!</h1>
                    {/* portfolio name */}
                    <input value='name of portfolio'/> <button onClick >delete Portfolio</button>

                </article>
            </section>
        );
    }

    return (
        <div>
            <UserHeader/>
            <Greeting/>
            <Container>
            <List celled>
    <List.Item>
        <List.Content floated="right">
        <Button icon="trash alternate" size="medium"/>
        </List.Content>
      <Image avatar src='/images/avatar/small/helen.jpg' />
      <List.Content>
        <List.Header>Snickerdoodle</List.Header>
        <Checkbox label='This checkbox comes pre-checked' defaultChecked />
        {/* portfolio.map((portfolio, index) => <portfolio key={index} portfolio={portfolio} />) */}
        An excellent companion
      </List.Content>
    </List.Item>
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

