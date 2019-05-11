import React from 'react';
import {Menu} from 'element-react/next';
import {withRouter} from 'react-router-dom';
import * as cssClasses from './sideMenu.module.css';

const SideMenu = (props) => {
  let items = null;
  if(props.companies) items = props.companies.map(c => <Menu.Item key={c.id} index={c.id.toString()}>{c.name}</Menu.Item>);
  return (
    <Menu defaultActive="1" className={`${cssClasses.sideMenu} el-menu-vertical-demo`}theme="dark">
      <Menu.Item index="0">All</Menu.Item>
      {items}
    </Menu>
  );
}

export default withRouter(SideMenu);