import { FC } from 'react'
import { ReactComponent as SearchSetting } from '@/assets/searchSetting.svg'
import styles from './SettingsImg.module.scss'
import classNames from 'classnames'

interface SettingsImgProps {
	isOpen: boolean
}

const SettingsImg: FC<SettingsImgProps> = ({ isOpen }) => {
	return (
		<SearchSetting
			className={classNames(styles.img, { [styles.imgColor]: isOpen })}
		/>
	)
}

export default SettingsImg
